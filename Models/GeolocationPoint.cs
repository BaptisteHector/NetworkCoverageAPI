namespace PaperNestTest.Models 
{
    /// <summary>
    /// Class used to store geolocation point;
    /// </summary>
    public class GeolocationPoint
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public GeolocationPoint(float latitude, float longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public float latitude {get;set;}
        public float longitude {get;set;}
    }
}