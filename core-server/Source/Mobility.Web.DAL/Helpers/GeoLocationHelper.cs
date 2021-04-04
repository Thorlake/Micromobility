using System;

namespace Mobility.Web.DAL.Helpers
{
    public class GeoLocationHelper
    {
        public static double ConvertDegreesToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }
    }
}
