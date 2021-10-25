using System;
using PaperNestTest.Enums;

namespace PaperNestTest.Models
{
    /// <summary>
    /// Class used to store network coverage of a provider.
    /// It represents the data extracted from the network coverage csv file.
    /// </summary>
    public class NetworkCoverage
    {

        public string           brandNumber { get; set; }
        public NetworkProvider  brandName   { get; set; }

        public double           latitude    { get; set; }
        public double           longitude   { get; set; }
        
        public bool             is2GCovered { get; set; }
        public bool             is3GCovered { get; set; }
        public bool             is4GCovered { get; set; }

        /// <summary>
        /// Method displaying the relevant data of the object.
        /// </summary>
        public void displayData()
        {
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("| BRANDNUMBER : " + brandNumber);
            Console.WriteLine("| BRANDNAME : " + brandName.ToString());
            Console.WriteLine("| LATITUDE : " + latitude.ToString());
            Console.WriteLine("| LONGITUDE : " + longitude.ToString());
            Console.WriteLine("| 2G : " + is2GCovered.ToString());
            Console.WriteLine("| 3G : " + is3GCovered.ToString());
            Console.WriteLine("| 4G : " + is4GCovered.ToString());
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("");
        }
    }
}