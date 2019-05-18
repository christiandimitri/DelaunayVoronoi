using System;
using System.Collections.Generic;

namespace DelaunayVoronoi
{
    public class Point
    {
        public double X;
        public double Y;
        public List<Triangle> AdjacentTriangles;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
            AdjacentTriangles = new List<Triangle>();
        }

        public override string ToString()
        {
            return "Point{" + X + "," + Y + "}";
        }
        public string ToCSV()
        {
            return X + "," + Y;
        }


        public static Point RandomPoint(Random RandGenerator, double MinValue, double MaxValue)
        {
            double range = MaxValue - MinValue;
            Point randomPoint = new Point(RandGenerator.NextDouble() * range + MinValue, RandGenerator.NextDouble() * range + MinValue);
            return randomPoint;
        }

    }
}