using System;
using System.Collections.Generic;
using PaperNestTest.Helpers;
using PaperNestTest.Models;

namespace PaperNestTest.Services
{
    /// <summary>
    /// Interface for NetworkCoverageService.
    /// </summary>
    public interface INetworkCoverageService
    {
        public IEnumerable<ProviderCoverage> GetByAddress(string address);
    }

    /// <summary>
    /// Class managing requests related with NetworkCoverage sent by the controller.
    /// </summary>
    public class NetworkCoverageService : INetworkCoverageService
    {

        /// <summary>
        /// Method giving the providers list to return to the GET request.
        /// i.e. the providers present around the address given in the query parameters.
        /// </summary>
        /// <param name="address">Address checked.</param>
        /// <returns>List of the providers around the address checked.</returns>
        public IEnumerable<ProviderCoverage> GetByAddress(string address)
        {
            Console.WriteLine("Converting address to geolocation ...");
            GeolocationPoint geolocation = AddressManagementHelper.convertAddressToGeolocation(address);
            if (geolocation == null)
            {
                Console.WriteLine("Address not found.");
                return null;
            }
            Console.WriteLine("Address converted.");

            Console.WriteLine("Checking the providers around...");
            List<ProviderCoverage> providerCoverage = new List<ProviderCoverage>();
            NetworkCoverageHelper.getNetworkCoverageOfAddress(geolocation, ref providerCoverage, 1000);
            Console.WriteLine(providerCoverage.Count.ToString() + " provider(s) found.");
            return providerCoverage;
        }
    }
}