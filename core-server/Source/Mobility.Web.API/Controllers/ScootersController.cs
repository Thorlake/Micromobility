using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mobility.Web.BLL.Polygon;
using Mobility.Web.BLL.Polygon.City;

namespace Mobility.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScootersController : ControllerBase
    {
        private readonly PolygonService _polygonService;
        private readonly ICityMapService _cityMapService;
        private readonly ILogger<ScootersController> _logger;

        public ScootersController(
            ICityMapService cityMapService,
            PolygonService polygonService,
            ILogger<ScootersController> logger)
        {
            _cityMapService = cityMapService;
            _polygonService = polygonService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<ScooterResponse> Get()
        {
            var cityPolygons = _cityMapService.GetPolygonPoints();
            var points = _polygonService.GetRandomPointsInsidePolygon(
                cityPolygons,
                45);

            var response = new ScooterResponse();
            response.Points = points
                .Select(d => new ScooterResponse.Point
                {
                    X = d.X,
                    Y = d.Y
                })
                .ToList();

            return Ok(response);
        }
    }
}
