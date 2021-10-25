using System;
using System.Collections.Generic;
using System.Net.Http;
using PaperNestTest.Models;
using PaperNestTest.Services;

namespace PaperNestTest.Helpers
{
    /// <summary>
    /// Static class managing address related process and conversion.
    /// </summary>
    public static class AddressManagementHelper
    {

        /// <summary>
        /// Method converting a string address to a GeolocationPoint.
        /// </summary>
        /// <param name="address">Address to convert.</param>
        /// <returns>The address converted in a GeolocationPoint.</returns>
        public static GeolocationPoint convertAddressToGeolocation(string address)
        {
            Console.WriteLine("Converting address : " + address);
            AdresseDataGouvService.Init();

            AdresseDataGouvReceiver adresseDataGouv = AdresseDataGouvService.GetAdressAsync(address).Result;
            if (adresseDataGouv == null)
                return null;
            if (adresseDataGouv.features.Length == 0)
                return null;

            adresseDataGouv.displayData();
            GeolocationPoint geolocation = new GeolocationPoint(adresseDataGouv.features[0].properties.y, adresseDataGouv.features[0].properties.x);

            Console.WriteLine("Address converted to geolocation point. Latitude : " + geolocation.latitude + " / Longitude : " + geolocation.longitude);
            return geolocation;
        }
    }
}