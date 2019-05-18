using System;
namespace DelaunayVoronoi
{
    public class Edge
    {

        public Point StartPoint;
        public Point EndPoint;

        public Edge(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
        public override string ToString()
        {
            return "Edge from " + StartPoint + " to " + EndPoint;
        }
        public string ToCSV()
        {
            return StartPoint.ToCSV() + "," + EndPoint.ToCSV();
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            var edge = obj as Edge;
            var samePoints = StartPoint == edge.StartPoint && EndPoint == edge.EndPoint;
            var samePointsReversed = StartPoint == edge.EndPoint && EndPoint == edge.StartPoint;
            return samePoints || samePointsReversed;
        }

        public override int GetHashCode()
        {
            int hCode = (int)StartPoint.X ^ (int)StartPoint.Y ^ (int)EndPoint.X ^ (int)EndPoint.Y;
            return hCode.GetHashCode();
        }



    }
}