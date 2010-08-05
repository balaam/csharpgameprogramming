using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Engine.PathFinding
{
    public class LineSegment
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public LineSegment(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public static Point GetMiddle(Point start, Point end)
        {
            return new Point(start.X + ((end.X - start.X) / 2.0f), start.Y + ((end.Y - start.Y) / 2.0f));
        }

        public Point GetMiddle()
        {
            return GetMiddle(Start, End);
        }

        public static float GetDistance(Point start, Point end, Point vertexPosition)
        {
            Point local = new Point(vertexPosition.X - end.X, vertexPosition.Y - end.Y);
            Point edge = new Point(start.X - end.X, start.Y - end.Y);
            float edgeLength = (float)Math.Sqrt(edge.X * edge.X + edge.Y * edge.Y);
            // Normalized edge.
            edge = new Point(edge.X / edgeLength, edge.Y / edgeLength);

            float nProj = local.Y * edge.X - local.X * edge.Y;
            float tProj = local.X * edge.X + local.Y * edge.Y;
            if (tProj < 0)
            {
                return (float) Math.Sqrt(tProj * tProj + nProj * nProj);
            }
            else if (tProj > edgeLength)
            {
                tProj -= edgeLength;
                return (float) Math.Sqrt(tProj * tProj + nProj * nProj);
            }
            else
            {
                return Math.Abs(nProj);
            }
        }

        public static bool Intersects(Point line1Start, Point line1End, Point line2Start, Point line2End, out Point intersect)
        {
            float x1 = line1Start.X;
            float y1 = line1Start.Y;
            float x2 = line1End.X;
            float y2 = line1End.Y;

            float x3 = line2Start.X;
            float y3 = line2Start.Y;
            float x4 = line2End.X;
            float y4 = line2End.Y;

            float denominator = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
            if (denominator == 0.0f)
            {
                intersect = new Point();
                return false;
            }

            float ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / denominator;
            float ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / denominator;

            if ((ua >= 0.0f && ua <= 1.0f) && (ub >= 0.0f && ub <= 1.0))
            {
                intersect = new Point(x1 + ua * (x2 - x1), y1 + ua * (y2 - y1));
                return true;
            }
            else
            {
                intersect = new Point();
                return false;
            }
        }

        public static float Length(Point start, Point end)
        {
            Point vectorLine = new Point(start.X - end.X, start.Y - end.Y);
            return (float)Math.Sqrt(vectorLine.X * vectorLine.X + vectorLine.Y * vectorLine.Y);
        }
    }
}
