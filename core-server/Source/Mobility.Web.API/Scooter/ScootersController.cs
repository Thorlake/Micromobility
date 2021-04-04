using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mobility.Web.BLL.Polygon;

namespace Mobility.Web.API.Scooter
{
    [ApiController]
    [Route("[controller]")]
    public class ScootersController : ControllerBase
    {
        private readonly ScooterLocationService _scooterLocationService;

        public ScootersController(
            ScooterLocationService scooterLocationService)
        {
            _scooterLocationService = scooterLocationService;
        }

        [HttpGet]
        public ActionResult<ScooterResponse> Get([FromQuery] GetScooterRequest request)
        {
            var points = _scooterLocationService.GetBy(
                request.Numbers,
                request.SearchRadiusMeters,
                request.LandmarkLongitude,
                request.LandmarkLatitude);

            var response = new ScooterResponse();
            response.Locations = points
                .Select(d => new ScooterResponse.ScooterLocation
                {
                    Longitude = d.Longitude,
                    Latitude = d.Latitude
                })
                .ToList();

            return Ok(response);
        }
    }
}
