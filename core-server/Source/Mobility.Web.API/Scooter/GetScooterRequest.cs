using System.ComponentModel.DataAnnotations;

namespace Mobility.Web.API.Scooter
{
    public class GetScooterRequest
    {
        [Required]
        public int Numbers { get; set; }
        
        [Required]
        public int SearchRadiusMeters { get; set; }

        [Required]
        public float LandmarkLongitude { get; set; }

        [Required]
        public float LandmarkLatitude { get; set; }
    }
}
