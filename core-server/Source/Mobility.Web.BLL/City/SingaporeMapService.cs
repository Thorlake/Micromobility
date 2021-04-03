using System;

namespace Mobility.Web.BLL.Polygon.City
{
    public class SingaporeMapService : ICityMapService
    {
        Point[] ICityMapService.GetPolygonPoints()
        {
            var points = new Point[] {
                new Point(103.6045074f, 1.2228228f),
                new Point(103.6374664f, 1.3457015f),
                new Point(103.6779785f, 1.4212104f),
                new Point(103.7143707f, 1.4445491f),
                new Point(103.814621f, 1.4644554f),
                new Point(104.0364075f, 1.3505066f),
                new Point(104.0130615f, 1.3079461f),
                new Point(103.9787292f, 1.3148107f),
                new Point(103.84552f, 1.257147f),
                new Point(103.6580658f, 1.2894114f),
                new Point(103.6395264f, 1.2084065f),
                new Point(103.6045074f, 1.2228228f)
            };

            // keep points oriented clockwise 
            Array.Reverse(points);
            return points;
        }
    }
}
