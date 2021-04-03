using System;

namespace Mobility.Web.BLL.Polygon
{
    public class Triangle : BaseShape
    {
        public Point LeftPoint { get; }
        public Point CurrentPoint { get; }
        public Point RightPoint { get; }

        public Triangle(Point leftPoint, Point currentPoint, Point rightPoint)
        {
            LeftPoint = leftPoint;
            CurrentPoint = currentPoint;
            RightPoint = rightPoint;
        }

        public bool IsPointInside(Point point)
        {
            var trianglePoints = new[] { LeftPoint, CurrentPoint, RightPoint };
            int lastTrianglePointIndex = trianglePoints.Length - 1;

            // Get the angle between the point and the
            // first and last vertices.
            float angle = GetAngle(
                trianglePoints[lastTrianglePointIndex],
                point,
                trianglePoints[0]);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < lastTrianglePointIndex; i++)
            {
                angle += GetAngle(
                    trianglePoints[i],
                    point,
                    trianglePoints[i + 1]);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Math.Abs(angle) > 1);
        }
    }
}
