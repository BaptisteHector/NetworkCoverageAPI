using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PaperNestTest.Models;

namespace PaperNestTest.Services
{
    /// <summary>
    /// Service static class conversing with the api https://adresse.data.gouv.fr/api-doc/adresse
    /// </summary>
    public static class AdresseDataGouvService
    {
        static HttpClient client = new HttpClient();
        
        /// <summary>
        /// Method initialisating the Client HTTP.
        /// </summary>
        public static void Init()
        {
            if (client.BaseAddress != null)
                return;
            Console.WriteLine("Initialising HTTP Client...");

            string uri = "https://api-adresse.data.gouv.fr/search/";

            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("HTTP Client Initialised. URI : " + uri);
        }

        /// <summary>
        /// Method proceding to a GET request to the API : https://adresse.data.gouv.fr/api-doc/adresse
        /// The API return all the information related with an address put as a parameter in the query.
        /// </summary>
        /// <param name="address">Address on which informations will be retreived.</param>
        /// <returns>The object filled with the informations given by the API</returns>
        public static async Task<AdresseDataGouvReceiver> GetAdressAsync(string address)
        {
            Console.WriteLine("GET api-adresse.data.gouv.fr data for address : " + address);
            if (address == "" || address == null)
                return null;

            AdresseDataGouvReceiver adresseDataGouv = new AdresseDataGouvReceiver();

            string addressForParams = address.Contains(' ') ? address.Replace(' ', '+') : address;
            string parameters = "?q=" + addressForParams + "&limit=1";
            HttpResponseMessage response = await client.GetAsync(parameters);

            Console.WriteLine("Request : " + response.RequestMessage.ToString());
            Console.WriteLine("Response Status : " + response.StatusCode.ToString());
            Console.WriteLine("Response isSuccessStatusStatus : " + response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Response Body : " + response.Content.ReadAsStreamAsync());
                adresseDataGouv = await response.Content.ReadAsAsync<AdresseDataGouvReceiver>();
            }

            return adresseDataGouv;
        }

    }
}