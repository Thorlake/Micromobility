using System.ComponentModel.DataAnnotations;

namespace Mobility.Web.DAL.Entities
{
    public class ScooterLocation
    {
        [Key]
        public long Id { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        // Not to perform converting each time
        public float RadiansLongitude { get; set; }
        public float RadiansLatitude { get; set; }
    }
}
