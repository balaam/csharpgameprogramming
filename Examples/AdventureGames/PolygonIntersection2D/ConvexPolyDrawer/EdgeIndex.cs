using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch.ConvexPolyDrawer
{
    struct EdgeIndex
    {
        public int Start;
        public int End;
        public EdgeIndex(int start, int end)
        {
            Start = start;
            End = end;
        }

        public static Point GetLineNormal(Point start, Point end)
        {
            Point result = new Point(end.Y - start.Y, -(end.X - start.X));
            float length;

            Vector2.Normalize(result, out length, out result);
     
            return result;

        }



        public static double GetDistance(Point start, Point end, Point point)
        {
            float edgeLength;

            Point local = Vector2.Subtract(point, end);
            Point edge = Vector2.Subtract(start, end);
            Vector2.Normalize(edge, out edgeLength, out edge);

            float nProj = local.Y * edge.X - local.X * edge.Y;
            float tProj = local.X * edge.X + local.Y * edge.Y;
            if (tProj < 0)
            {
                return Math.Sqrt(tProj * tProj + nProj * nProj);
            }
            else if (tProj > edgeLength)
            {
                tProj -= edgeLength;
                return Math.Sqrt(tProj * tProj + nProj * nProj);
            }
            else
            {
                return Math.Abs(nProj);
            }

        }

        public static float Length(Point start, Point end)
        {
            return Vector2.Length(Vector2.Subtract(end, start));
        }


        public static Point GetClosestPoint(Point start, Point end, Point point)
        {
            Point c = Vector2.Subtract(point, start);
            Point v = Vector2.Subtract(end, start);
            float d = Vector2.Length(v);
            v = new Point(v.X / d, v.Y / d);
           // float t = v.DotProduct(c);
            float t = Vector2.DotProduct(v, c);

            if (t < 0.0f) return start;
            if (t > d) return end;

            v = new Point(v.X * t, v.Y * t);
            return new Point(start.X + v.X, start.Y + v.Y);
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


    }
}
