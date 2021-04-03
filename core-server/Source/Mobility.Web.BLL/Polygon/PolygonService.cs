using System;
using System.Collections.Generic;
using System.Linq;

namespace Mobility.Web.BLL.Polygon
{
    // How to find random points in a polygon: https://observablehq.com/@scarysize/finding-random-points-in-a-polygon
    // Polygon triangulation is based on codebase of http://www.csharphelper.com/examples/howto_polygon_geometry.zip
    public class PolygonService
    {
        static Random randomGenerator = new Random();

        public Point[] GetRandomPointsInsidePolygon(
            Point[] polygonPoints,
            int numberOfPoints)
        {
            var triangles = GetTriangles(polygonPoints);
            var points = new List<Point>();
            for (int i = 0; i < numberOfPoints; i++)
            {
                var randomTriangle = GetRandomTriangle(triangles);
                var randomPoint = GetRandomPoint(randomTriangle);
                points.Add(randomPoint);
            }
            return points.ToArray();
        }

        private List<Triangle> GetTriangles(Point[] points)
        {
            var clonedPoints = new Point[points.Length];
            Array.Copy(points, clonedPoints, points.Length);
            Polygon polygon = new Polygon(clonedPoints);

            var triangles = new List<Triangle>();
            while (polygon.Points.Length > 3)
            {
                var (ear, earIndex) = polygon.FindEar();
                triangles.Add(new Triangle(ear.LeftPoint, ear.CurrentPoint, ear.RightPoint));
                polygon.RemoveEarFromPointsByIndex(earIndex);
            }
            triangles.Add(new Triangle(polygon.Points[0], polygon.Points[1], polygon.Points[2]));

            return triangles;
        }

        private Triangle GetRandomTriangle(List<Triangle> triangles)
        {
            var cumulativeDistribution = GetGeneratedDistribution(triangles);
            var randomValueFromZeroToOne = (float)randomGenerator.NextDouble();
            var triangleIndex = cumulativeDistribution.FindIndex(areaSum => areaSum > randomValueFromZeroToOne);
            return triangles[triangleIndex];
        }

        private List<float> GetGeneratedDistribution(List<Triangle> triangles)
        {
            var totalArea = triangles.Sum(t => GetTriangleArea(t));

            var prevValue = 0f;
            var cumulativeDistribution = new List<float>();
            for (var i = 0; i < triangles.Count(); i++)
            {
                var nextValue = prevValue + GetTriangleArea(triangles[i]) / totalArea;
                cumulativeDistribution.Add(nextValue);
                prevValue = nextValue;
            }

            // [area1, area1 + aera2, area1 + area2 + area3, ...]
            return cumulativeDistribution;
        }

        private float GetTriangleArea(Triangle triangle)
        {
            float AxByCy = triangle.LeftPoint.X * (triangle.CurrentPoint.Y - triangle.RightPoint.Y);
            float BxCyAy = triangle.CurrentPoint.X * (triangle.RightPoint.Y - triangle.LeftPoint.Y);
            float CxAyBy = triangle.RightPoint.X * (triangle.LeftPoint.Y - triangle.CurrentPoint.Y);
            return (AxByCy + BxCyAy + CxAyBy) / 2;
        }

        private Point GetRandomPoint(Triangle triangle)
        {
            // Random position = A + R * AB + S * AC
            var Ax = triangle.LeftPoint.X;
            var Ay = triangle.LeftPoint.Y;
            var R = (float)randomGenerator.NextDouble();
            var S = (float)(randomGenerator.NextDouble() * (1 - R));

            // AB = [Bx – Ax, By – Ay]
            // AC = [Cx – Ax, Cy – Ay].
            var ABx = triangle.CurrentPoint.X - triangle.LeftPoint.X;
            var ABy = triangle.CurrentPoint.Y - triangle.LeftPoint.Y;
            var ACx = triangle.RightPoint.X - triangle.LeftPoint.X;
            var ACy = triangle.RightPoint.Y - triangle.LeftPoint.Y;

            var x = Ax + R * ABx + S * ACx;
            var y = Ay + R * ABy + S * ACy;
            return new Point(x, y);
        }
    }

}
