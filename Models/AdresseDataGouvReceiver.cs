using System;
using System.Collections.Generic;

namespace PaperNestTest.Models
{
    /// <summary>
    /// Class receiving the data from api-adresse.data.gouv.fr call
    /// https://adresse.data.gouv.fr/api-doc/adresse
    /// </summary>
    public class AdresseDataGouvReceiver
    {
        public string type { get; set; }
        public string version {get;set;}
        public Feature[] features {get;set;}
        public string attribution { get; set; }
        public string licence { get; set; }
        public string query { get; set; }
        public int limit { get; set; }

        /// <summary>
        /// Method displaying the relevant data of the object.
        /// </summary>
        public void displayData()
        {
            Console.WriteLine("");
            Console.WriteLine("----- Data for address : " + query + " -----");
            Console.WriteLine("| ID : " + features[0].properties.id);
            Console.WriteLine("| X : " + features[0].properties.x.ToString());
            Console.WriteLine("| Y : " + features[0].properties.y.ToString());
            Console.WriteLine("| HOUSENUMBER : " + features[0].properties.housenumber);
            Console.WriteLine("| POSTCODE : " + features[0].properties.postcode);
            Console.WriteLine("| CITY : " + features[0].properties.city);
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("");
        }
    }

    public class Feature
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Property properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public float[] coordinates { get; set; }
    }

    public class Property
    {
        public string label { get; set; }
        public float score { get; set; }
        public string housenumber { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string postcode { get; set; }
        public string citycode { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string context { get; set; }
        public string type { get; set; }
        public float importance { get; set; }
        public string street { get; set; }
    }
}