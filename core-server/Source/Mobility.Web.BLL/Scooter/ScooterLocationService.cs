using Mobility.Web.BLL.Scooter;
using Mobility.Web.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Mobility.Web.BLL.Polygon
{
    public class ScooterLocationService
    {
        private readonly ScooterLocationRepository _scooterLocationRepository;

        public ScooterLocationService(ScooterLocationRepository scooterLocationRepository)
        {
            _scooterLocationRepository = scooterLocationRepository;
        }

        public IEnumerable<ScooterLocation> GetBy(
           int scootersNumber,
           int searchingRadiusMeters,
           float landmarkLongitude,
           float landmarkLatitude)
        {
            var searchingRadiusKilometer = searchingRadiusMeters / 1000;
            var scooterLocations = _scooterLocationRepository.GetBy(
                scootersNumber,
                searchingRadiusKilometer,
                landmarkLongitude,
                landmarkLatitude);
            var result = scooterLocations.Select(location =>
            {
                return new ScooterLocation
                {
                    Longitude = location.Longitude,
                    Latitude = location.Latitude
                };
            });
            return result;
        }
    }

}
