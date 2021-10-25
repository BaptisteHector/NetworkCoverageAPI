using System;
using System.Collections.Generic;
using System.IO;
using lambertcs;
using PaperNestTest.Class;
using PaperNestTest.Enums;
using PaperNestTest.Models;

namespace PaperNestTest.Helpers
{

    /// <summary>
    /// Static class managing network coverage related processes and conversions.
    /// </summary>
    public static class NetworkCoverageHelper
    {
        /// <summary>
        /// Path to the CSV file from which the API gets the network coverage data.
        /// </summary>
        static string filePath = @"Utils\NetworkCoverageCSV.csv";

        /// <summary>
        /// Method reading the network coverage file and filling the list with the raw data.
        /// </summary>
        /// <param name="rawNetworkCoverageRows">Reference. String list to fill with the network coverage data.</param>
        public static void rawNetworkCoverageRows(ref List<string> rawNetworkCoverageRows)
        {
            Console.WriteLine("Reading file : " + filePath + " ...");
            using (var sreader = new StreamReader(filePath))
            {
                string[] headers = sreader.ReadLine().Split('\n');
                while (!sreader.EndOfStream)
                {
                    rawNetworkCoverageRows.Add(sreader.ReadLine());
                }
            }
            Console.WriteLine("StreamReader closed.");
        }

        /// <summary>
        /// Method converting a row of raw data in a NetworkCoverage.
        /// </summary>
        /// <param name="networkCoverage">Target object to generate with the data from the row.</param>
        /// <param name="row">Source string for the data to be converted.</param>
        /// <returns>Boolean showing if the conversion succeed or not.</returns>
        public static bool putRowinList(ref NetworkCoverage networkCoverage, string row)
        {
            try
            {
                string[] rowValues = row.Split(';');

                // Checking if the data can be converted to the 
                if (!checkDataQuality(rowValues))
                    return false;
                                
                networkCoverage.brandNumber = rowValues[0];

                // If the provider cannot be found in the dictionary, object is still created with UNKNOWN provider name.
                networkCoverage.brandName = (NetworkProvidersDictionary.dico.ContainsKey(rowValues[0]) ? NetworkProvidersDictionary.dico[rowValues[0]] : NetworkProvider.UNKNOWN);

                networkCoverage.longitude = double.Parse(rowValues[1]);
                networkCoverage.latitude = double.Parse(rowValues[2]);

                networkCoverage.is2GCovered = (rowValues[3] == "1" || rowValues[3] == "true");
                networkCoverage.is3GCovered = (rowValues[4] == "1" || rowValues[4] == "true");
                networkCoverage.is4GCovered = (rowValues[5] == "1" || rowValues[5] == "true");
                //networkCoverage.displayData();
                return true;
            }
            catch(InvalidDataException e)
            {
                Console.WriteLine("ERROR catched : " + e);
                return false;
            }
        }

        /// <summary>
        /// Method checking if the data taken from the network coverage file will fit NetworkCoverage.
        /// </summary>
        /// <param name="rowValues">Array of row values to check.</param>
        /// <returns>Boolean showing if the data is proper or not.</returns>
        private static bool checkDataQuality(string[] rowValues)
        {
            // CHeck if the right amount of values are present.
            if (rowValues.Length != 6)
                return false;
            
            // Check if both coordinate point are double.
            if (!double.TryParse(rowValues[1], out double longitude))
                return false;
            if (!double.TryParse(rowValues[2], out double latitude))
                return false;

            return true;
        }

        /// <summary>
        /// Method filling a list of network providers present around the geolocation point.
        /// The method takes maximum once each provider with the closest location.
        /// </summary>
        /// <param name="geolocation">Geolocation point to check.</param>
        /// <param name="providerCoverage">List to fill with the providers present around.</param>
        /// <param name="approximation">Approximation of the geolocation point to find the providers.</param>
        /// <returns>Boolean showing if there is any provider around the geolocation point or not.</returns>
        public static bool getNetworkCoverageOfAddress(GeolocationPoint geolocation, ref List<ProviderCoverage> providerCoverage, double approximation)
        {
            bool result = false;

            // Reading all the list of network coverage.
            foreach(NetworkCoverage networkCoverage in NetworkCoverageSingleton.Instance.NetworkCoverageList)
            {

                if (isWithinCoverage(networkCoverage, geolocation, approximation))
                {
                    // Managing approximation.
                    double latitudeApproximation = networkCoverage.latitude - geolocation.latitude;
                    double longitudeApproximation = networkCoverage.longitude - geolocation.longitude;
                    double globalApproximation = Math.Abs(longitudeApproximation) + Math.Abs(latitudeApproximation);

                    if (!isFurtherOrAbsent(ref providerCoverage, networkCoverage, globalApproximation))
                        continue;

                    ProviderCoverage newProviderCoverageItem = new ProviderCoverage(networkCoverage.brandName.ToString(),
                                                                                    networkCoverage.is2GCovered,
                                                                                    networkCoverage.is3GCovered,
                                                                                    networkCoverage.is4GCovered,
                                                                                    globalApproximation);

                    providerCoverage.Add(newProviderCoverageItem);
                    result = true;
                    Console.WriteLine("Provider " + newProviderCoverageItem.brandName +
                                        " added to the list with global approximation " +
                                        newProviderCoverageItem.globalApproximation + ".");
                }
            }
            return result;
        }

        /// <summary>
        /// Method checking if the provider is already in the list and, if yes, if this one is closer to the geolocation point than the present one.
        /// </summary>
        /// <param name="providerCoverage">List filled with the providers</param>
        /// <param name="networkCoverage">NetworkCoverage to check if it should be added to the list.</param>
        /// <param name="globalApproximation">The approximation of the NetworkCoverage checkeg. Used to spot if this one is closer than the present one.</param>
        /// <returns>Boolean showing if the checked provider is not in the list or if the last data is further than the new one.</returns>
        private static bool isFurtherOrAbsent(ref List<ProviderCoverage> providerCoverage, NetworkCoverage networkCoverage, double globalApproximation)
        {
            // Check if the provider is already inside the list.
            if (!providerCoverage.Exists(item => (item.brandName == networkCoverage.brandName.ToString())))
                return true;
            
            ProviderCoverage existingProviderCoverage = providerCoverage.Find(item => (item.brandName == networkCoverage.brandName.ToString()));

            // Check if the existing provider is closer than the new one.
            if (existingProviderCoverage.globalApproximation > globalApproximation)
            {
                providerCoverage.Remove(existingProviderCoverage);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method checking if a network coverage is within the area, defined by the approximation, around the geolocation point.
        /// </summary>
        /// <param name="networkCoverage">Network coverage to check if it is close to the geolocation point.</param>
        /// <param name="geolocation">Base geolocation point.</param>
        /// <param name="approximation">Approximation wanted to define the accepted area.</param>
        /// <returns>Boolean if the network coverage is within the area of the geolocation or not.</returns>
        private static bool isWithinCoverage(NetworkCoverage networkCoverage, GeolocationPoint geolocation, double approximation)
        {
            double minimumLatitude = networkCoverage.latitude - approximation;
            double maximumLatitude = networkCoverage.latitude + approximation;
            bool isLatitudeInCoverage = (minimumLatitude <= geolocation.latitude) && (geolocation.latitude <= maximumLatitude);

            double minimumLongitude = networkCoverage.longitude - approximation;
            double maximumLongitude = networkCoverage.longitude + approximation;
            bool isLongitudeInCoverage = (minimumLongitude <= geolocation.longitude) && (geolocation.longitude <= maximumLongitude);

            return (isLatitudeInCoverage && isLongitudeInCoverage);
        }
    }
}