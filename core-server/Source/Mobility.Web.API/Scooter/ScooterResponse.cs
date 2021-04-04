using System.Collections.Generic;

namespace Mobility.Web.API.Scooter
{
    public class ScooterResponse
    {
        public List<ScooterLocation> Locations { get; set; }

        public class ScooterLocation
        {
            public float Longitude { get; set; }
            public float Latitude { get; set; }
        }
    }
}
