using System;
using System.Collections.Generic;

namespace Mobility.Web.BLL.Polygon
{
    public class Polygon : BaseShape
    {
        public Point[] Points { get; set; }

        public Polygon(Point[] points)
        {
            Points = points;
        }

        public (Triangle ear, int earIndex) FindEar()
        {
            int leftPointIndex = 0;
            int currentPointIndex = 0;
            int rightPointIndex = 0;
            if(!IsEarFound(ref leftPointIndex, ref currentPointIndex, ref rightPointIndex))
            {
                throw new Exception("Ear is not found during polygon triangulation");
            }

            var ear = new Triangle(Points[leftPointIndex], Points[currentPointIndex], Points[rightPointIndex]);
            return (ear, currentPointIndex);
        }

        public void RemoveEarFromPointsByIndex(int indexToSkip)
        {
            Point[] clonedPoints = new Point[Points.Length - 1];
            Array.Copy(Points, 0, clonedPoints, 0, indexToSkip);
            Array.Copy(Points, indexToSkip + 1, clonedPoints, indexToSkip, Points.Length - indexToSkip - 1);
            Points = clonedPoints;
        }

        private bool IsEarFound(ref int leftPointIndex, ref int currentPointIndex, ref int rightPointIndex)
        {
            int pointsLength = Points.Length;
            for (leftPointIndex = 0; leftPointIndex < pointsLength; leftPointIndex++)
            {
                currentPointIndex = (leftPointIndex + 1) % pointsLength;
                rightPointIndex = (currentPointIndex + 1) % pointsLength;

                if (IsEar(Points, leftPointIndex, currentPointIndex, rightPointIndex))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsEar(Point[] points, int leftPointIndex, int currentPointIndex, int rightPointIndex)
        {
            var angle = GetAngle(
                points[leftPointIndex],
                points[currentPointIndex],
                points[rightPointIndex]
            );
            var isAngleConcave = angle > 0;
            if (isAngleConcave)
            {
                return false;
            }

            var triangle = new Triangle(points[leftPointIndex], points[currentPointIndex], points[rightPointIndex]);
            return IsAnotherPointInsideTriangle(
                triangle,
                points,
                leftPointIndex,
                currentPointIndex,
                rightPointIndex);
        }

        private bool IsAnotherPointInsideTriangle(
            Triangle triangle,
            Point[] points,
            int leftPointIndex,
            int currentPointIndex,
            int rightPointIndex)
        {
            for (int pointIndex = 0; pointIndex < points.Length; pointIndex++)
            {
                if ((pointIndex != leftPointIndex) && (pointIndex != currentPointIndex) && (pointIndex != rightPointIndex))
                {
                    if (triangle.IsPointInside(points[pointIndex]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

       
    }
}
