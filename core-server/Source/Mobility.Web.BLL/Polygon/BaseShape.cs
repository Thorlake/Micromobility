using System;

namespace Mobility.Web.BLL.Polygon
{
    public abstract class BaseShape
    {
        protected float GetAngle(
            Point leftPoint,
            Point curPoint,
            Point rightPoint)
        {
            // http://csharphelper.com/blog/2020/06/find-the-angle-between-two-vectors-in-c/
            float dot_product = DotProduct(leftPoint, curPoint, rightPoint);
            float cross_product = CrossProduct(leftPoint, curPoint, rightPoint);
            return (float)Math.Atan2(cross_product, dot_product);
        }

        private static float DotProduct(
            Point leftPoint,
            Point curPoint,
            Point rightPoint)
        {
            float leftCurrentX = leftPoint.X - curPoint.X;
            float leftCurrentY = leftPoint.Y - curPoint.Y;
            float rightCurrentX = rightPoint.X - curPoint.X;
            float rightCurrentY = rightPoint.Y - curPoint.Y;

            return leftCurrentX * rightCurrentX + leftCurrentY * rightCurrentY;
        }

        public static float CrossProduct(
            Point leftPoint,
            Point curPoint,
            Point rightPoint)
        {
            float leftCurrentX = leftPoint.X - curPoint.X;
            float leftCurrentY = leftPoint.Y - curPoint.Y;
            float rightCurrentX = rightPoint.X - curPoint.X;
            float rightCurrentY = rightPoint.Y - curPoint.Y;

            return leftCurrentX * rightCurrentY - leftCurrentY * rightCurrentX;
        }
    }
}
