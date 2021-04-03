using System.Collections.Generic;

namespace Mobility.Web.API.Controllers
{
    public class ScooterResponse
    {
        public List<Point> Points { get; set; }

        public class Point
        {
            public float X { get; set; }
            public float Y { get; set; }
        }
    }
}
