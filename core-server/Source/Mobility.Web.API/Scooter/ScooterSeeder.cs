using Mobility.Web.BLL.Polygon;
using Mobility.Web.BLL.Polygon.City;
using System;
using Microsoft.Extensions.DependencyInjection;
using Mobility.Web.DAL.Helpers;
using Mobility.Web.DAL;
using Mobility.Web.DAL.Entities;

namespace Mobility.Web.API.Scooter
{
    public class ScooterSeeder
    {
        private readonly int _scooterNumbersToGenerate = 1000;

        public void Seed(IServiceProvider serviceProvider)
        {
            var polygonService = serviceProvider.GetService<PolygonService>();
            var cityMapService = serviceProvider.GetService<ICityMapService>();
            var context = serviceProvider.GetService<MobilityDbContext>();

            var cityPolygons = cityMapService.GetPolygonPoints();
            var points = polygonService.GetRandomPointsInsidePolygon(
                cityPolygons,
                _scooterNumbersToGenerate);

            foreach (Point point in points)
            {
                context.ScooterLocations.Add(new ScooterLocation
                {
                    Longitude = point.X,
                    Latitude = point.Y,
                    RadiansLongitude = (float)GeoLocationHelper.ConvertDegreesToRadians(point.X),
                    RadiansLatitude = (float)GeoLocationHelper.ConvertDegreesToRadians(point.Y)
                });
            }

            context.SaveChanges();
        }
    }
}
