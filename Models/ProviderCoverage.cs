namespace PaperNestTest.Models
{
    /// <summary>
    /// Class used to store the data of the providers network coverage.
    /// It is the object which is returned as an answer to the api request.
    /// </summary>
    public class ProviderCoverage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="brandName"></param>
        /// <param name="is2GCovered"></param>
        /// <param name="is3GCovered"></param>
        /// <param name="is4GCovered"></param>
        /// <param name="globalApproximation"></param>
        public ProviderCoverage(string brandName, bool is2GCovered, bool is3GCovered, bool is4GCovered, double globalApproximation)
        {
            this.brandName = brandName;
            this.is2GCovered = is2GCovered;
            this.is3GCovered = is3GCovered;
            this.is4GCovered = is4GCovered;
            this.globalApproximation = globalApproximation;
        }

        public string           brandName { get; set; }
        public bool             is2GCovered { get; set; }
        public bool             is3GCovered { get; set; }
        public bool             is4GCovered { get; set; }
        public double           globalApproximation {get; set;}
    }
}