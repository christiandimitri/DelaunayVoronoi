using System;
using System.Collections.Generic;
namespace DelaunayVoronoi
{
    public class Triangle
    {

        public List<Point> Vertices = new List<Point>();

        public Point Circumcenter;
        public double RadiusSquared;

        public List<Triangle> TrianglesWithSharedEdges()
        {
            List<Triangle> neighbours = new List<Triangle>();
            foreach (Point vertex in Vertices)
            {
                foreach (Triangle sharedTriangle in vertex.AdjacentTriangles)
                {
                    if (this.SharesEdgeWidth(sharedTriangle) && neighbours.Contains(sharedTriangle) == false && sharedTriangle != this) neighbours.Add(sharedTriangle);

                }
            }
            return neighbours;
        }
        public Triangle(Point point1, Point point2, Point point3)
        {

            Vertices = new List<Point>();
            if (!IsCounterClockwise(point1, point2, point3))
            {
                Vertices.Add(point1);
                Vertices.Add(point3);
                Vertices.Add(point2);
            }
            else
            {
                Vertices.Add(point1);
                Vertices.Add(point2);
                Vertices.Add(point3);
            }
            Vertices[0].AdjacentTriangles.Add(this);
            Vertices[1].AdjacentTriangles.Add(this);
            Vertices[2].AdjacentTriangles.Add(this);
            UpdateCircumcircle();

        }
        public void UpdateCircumcircle()
        {
            Point p0 = Vertices[0];
            Point p1 = Vertices[1];
            Point p2 = Vertices[2];
            double dA = p0.X * p0.X + p0.Y * p0.Y;
            double dB = p1.X * p1.X + p1.Y * p1.Y;
            double dC = p2.X * p2.X + p2.Y * p2.Y;

            double aux1 = (dA * (p2.Y - p1.Y) + dB * (p0.Y - p2.Y) + dC * (p1.Y - p0.Y));
            double aux2 = -(dA * (p2.X - p1.X) + dB * (p0.X - p2.X) + dC * (p1.X - p0.X));
            double div = (2 * (p0.X * (p2.Y - p1.Y) + p1.X * (p0.Y - p2.Y) + p2.X * (p1.Y - p0.Y)));

            if (div == 0)
            {
                throw new System.Exception();
            }

            Point center = new Point(aux1 / div, aux2 / div);
            Circumcenter = center;
            RadiusSquared = (center.X - p0.X) * (center.X - p0.X) + (center.Y - p0.Y) * (center.Y - p0.Y);
        }

        public bool IsCounterClockwise(Point point1, Point point2, Point point3)
        {
            double result = (point2.X - point1.X) * (point3.Y - point1.Y) -
                (point3.X - point1.X) * (point2.Y - point1.Y);
            return result > 0;
        }

        public bool SharesEdgeWidth(Triangle triangle)
        {
            int sharedCount = 0;
            foreach (Point vertex in this.Vertices)
            {
                foreach (Point vertex2 in triangle.Vertices)
                {
                    if (vertex == vertex2) sharedCount++;
                }
            }
            return sharedCount == 2;

            throw new NotImplementedException();
        }
        public bool IsPointInsideCircumcircle(Point point)
        {
            double d_squared = (point.X - Circumcenter.X) * (point.X - Circumcenter.X) +
                            (point.Y - Circumcenter.Y) * (point.Y - Circumcenter.Y);
            return d_squared < RadiusSquared;
        }

        public override string ToString()
        {
            return "Triangle{ A: " + Vertices[0] + " B: " + Vertices[1] + " C: " + Vertices[2] + " }";
        }
        public string ToCSV()
        {
            return Vertices[0].ToCSV() + "," + Vertices[1].ToCSV() + "," + Vertices[2].ToCSV();
        }
    }
}