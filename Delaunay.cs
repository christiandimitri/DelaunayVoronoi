using System;
using System.Collections.Generic;
using System.Linq;

namespace DelaunayVoronoi
{
    public class Delaunay
    {
        public double MaxX;
        public double MaxY;
        List<Triangle> Triangulation;
        public List<Triangle> border;
        List<Edge> VoronoiEdges;
        public Delaunay( )
        {
            Triangulation = new List<Triangle>();
            border = new List<Triangle>();
            VoronoiEdges = new List<Edge>();
        }

        public List<Point> GeneratePoints(int amout, double maxX, double maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
            Point point0 = new Point(0, 0);
            Point point1 = new Point(0, MaxY);
            Point point2 = new Point(MaxX, MaxY);
            Point point3 = new Point(MaxX, 0);
            List<Point> points = new List<Point>() { point0, point1, point2, point3 };
            Triangle triangle1 = new Triangle(point0, point1, point2);
            Triangle triangle2 = new Triangle(point0, point2, point3);
            border = new List<Triangle> { triangle1, triangle2 };
            Random rnd = new Random();
            List<Point> points2 = new List<Point>();
            for (int i = 0; i < amout - 4; i++)
            {
                points2.Add(Point.RandomPoint(rnd, 0, maxX));
            }
            return points2;
        }
        public void BowerWatson(List<Point> points)
        {
            List<Triangle> triangulation = new List<Triangle>(border);
            points.Reverse();
            foreach (Point point in points)
            {
                List<Triangle> badTriangles = FindBadTriangles(point, triangulation);
                List<Edge> polygon = FindHoleBoundaries(badTriangles);
                foreach (Triangle triangle in badTriangles)
                {
                    foreach (Point vertex in triangle.Vertices)
                    {
                        vertex.AdjacentTriangles.Remove(triangle);
                    }
                    if (triangulation.Contains(triangle)) triangulation.Remove(triangle);
                }
                foreach (Edge edge in polygon)
                {
                    Triangle triangle = new Triangle(point, edge.StartPoint, edge.EndPoint);
                    triangulation.Add(triangle);
                }
            }
            Triangulation = triangulation;
        }
        public string DelaunayToCSV()
        {
            string CSV="";

            foreach(Triangle triangle in Triangulation)
            {
                CSV += "\n" + triangle.ToCSV();
                
            }
            return CSV;
        }
        public List<Edge> FindHoleBoundaries(List<Triangle> badTriangles)
        {
            List<Edge> boundaryEdges = new List<Edge>();
            List<Edge> duplicateEdges = new List<Edge>();
            foreach (Triangle triangle in badTriangles)
            {
                Edge e = new Edge(triangle.Vertices[0], triangle.Vertices[1]);
                if (!boundaryEdges.Contains(e)) 
                boundaryEdges.Add(e);
                else 
                duplicateEdges.Add(e);
                Edge f = new Edge(triangle.Vertices[1], triangle.Vertices[2]);
                if (!boundaryEdges.Contains(f)) 
                boundaryEdges.Add(f);
                else 
                duplicateEdges.Add(f);
                Edge j = new Edge(triangle.Vertices[2], triangle.Vertices[0]);
                if (!boundaryEdges.Contains(j)) 
                boundaryEdges.Add(j);
                else 
                duplicateEdges.Add(j);
            }

            for (int i = boundaryEdges.Count - 1; i >= 0;i--)
            {
                Edge e = boundaryEdges[i];
                if (duplicateEdges.Contains(e))
                    boundaryEdges.Remove(e);
            }
            return boundaryEdges;
        }

        public void GenerateSupraTriangle()
        {

        }

        public List<Triangle> FindBadTriangles(Point point, List<Triangle> triangles)
        {
            List<Triangle> badTriangles = new List<Triangle>();
            foreach (Triangle triangle in triangles)
            {
                if (triangle.IsPointInsideCircumcircle(point)) badTriangles.Add(triangle);
            }
            return badTriangles;
        }


        public void GenerateVoronoiFromDelaunay()
        {
            List<Edge> voronoiEdges = new List<Edge>();
            foreach(Triangle triangle in Triangulation)
            {
                foreach(Triangle neigbour in triangle.TrianglesWithSharedEdges())
                {
                    Edge edge = new Edge(triangle.Circumcenter, neigbour.Circumcenter);
                    voronoiEdges.Add(edge);

                }

            }
            VoronoiEdges= voronoiEdges;

        }
        public string VoronoiToCSV()
        {
            string CSV = "";

            foreach (Edge edge in VoronoiEdges)
            {
                CSV += "\n" + edge.ToCSV();

            }
            return CSV;
        }
    }
}