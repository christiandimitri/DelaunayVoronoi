using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DelaunayVoronoi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Point pt1 = new Point(15, 1.0);
            Point pt2 = new Point(0.8, 2.5);
            Point pt3 = new Point(5, 10.0);
            Edge edge = new Edge(pt1, pt2);
            Triangle triangle = new Triangle(pt1, pt2, pt3);

            Stopwatch stp = new Stopwatch();
            stp.Start();
            Delaunay delaunay = new Delaunay();

            List<Point> randomPoints = delaunay.GeneratePoints(150, 100, 100);
            delaunay.BowerWatson(randomPoints);
            delaunay.GenerateVoronoiFromDelaunay();
            stp.Stop();
            Console.WriteLine("Elapsed time: {0}", stp.Elapsed);
            File.WriteAllText("DelaunayResults.csv", delaunay.DelaunayToCSV());
            File.WriteAllText("VoronoiResults.csv", delaunay.VoronoiToCSV());



            Console.WriteLine(triangle);
            // File.WriteAllText("Results.csv", triangle.ToString());
            // Console.WriteLine(edge.ToCSV());
            // File.WriteAllText("Results.csv", edge.ToCSV());



        }
    }
}
