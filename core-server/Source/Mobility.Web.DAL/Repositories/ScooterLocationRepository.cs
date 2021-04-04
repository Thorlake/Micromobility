using Mobility.Web.DAL.Entities;
using Mobility.Web.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mobility.Web.DAL.Repositories
{
    public class ScooterLocationRepository
    {
        private readonly MobilityDbContext _context;

        public ScooterLocationRepository(MobilityDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ScooterLocation> GetBy(
            int scootersNumber,
            int searchingRadiusKilometer,
            float landmarkLongitude,
            float landmarkLatitude)
        {
            int earthRadiusKilometer = 6371;
            var radiansLongitude = GeoLocationHelper.ConvertDegreesToRadians(landmarkLongitude);
            var radiansLatitude = GeoLocationHelper.ConvertDegreesToRadians(landmarkLatitude);

            // The great circle distance
            // d = acos(sin(lat1)*sin(lat2)+cos(lat1)*cos(lat2)*cos(lon1-lon2))
            return _context.ScooterLocations
                .Select(location => new
                {
                    Location = location,
                    Distance = earthRadiusKilometer *
                        Math.Acos(
                            Math.Sin(radiansLatitude) * Math.Sin(location.RadiansLatitude)
                            +
                            (
                                Math.Cos(radiansLatitude) * Math.Cos(location.RadiansLatitude)
                                * Math.Cos(radiansLongitude - location.RadiansLongitude)
                            )
                        ),
                })
                .Where(location => location.Distance < searchingRadiusKilometer)
                .OrderBy(location => location.Distance)
                .Take(scootersNumber)
                .Select(s => s.Location);
        }
    }
}
