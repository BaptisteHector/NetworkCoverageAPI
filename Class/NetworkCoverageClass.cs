using System;
using System.Collections.Generic;
using PaperNestTest.Helpers;
using PaperNestTest.Models;

namespace PaperNestTest.Class
{
    /// <summary>
    /// Singleton giving access to the network coverage list.
    /// List being generated from the csv file "NetworkCoverageCSV.csv" located in Utils folder.
    /// </summary>
    public class NetworkCoverageSingleton
    {
        private static NetworkCoverageSingleton _instance;
        static readonly object instanceLock = new object();

        /// <summary>
        /// Network coverage list generated from the csv file.
        /// </summary>
        /// <value></value>
        public List<NetworkCoverage> NetworkCoverageList {get; set;}

        /// <summary>
        /// Singleton's constructor.
        /// Getting data from the helper to generate the list.
        /// Each row in the csv file which respect the syntax will be an item in the list.
        /// </summary>
        private NetworkCoverageSingleton() 
        {
            Console.WriteLine("Initialising the Singleton...");
            
            List<string> rawNetworkCoverageRows = new List<string>();
            NetworkCoverageHelper.rawNetworkCoverageRows(ref rawNetworkCoverageRows);

            NetworkCoverageList = new List<NetworkCoverage>();
            foreach(string row in rawNetworkCoverageRows)
            {
                NetworkCoverage networkCoverage = new NetworkCoverage();
                if (NetworkCoverageHelper.putRowinList(ref networkCoverage, row))
                    NetworkCoverageList.Add(networkCoverage);
            }

            Console.WriteLine("Singleton initialised. List contains " + NetworkCoverageList.Count + " items.");
        }

        /// <summary>
        /// Instance of the singleton.
        /// Gets created when called if the instance doesn't already exist.
        /// </summary>
        /// <value></value>
        public static NetworkCoverageSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new NetworkCoverageSingleton();
                        }
                    }
                }
                return _instance;
            }
        }
    }
    
}