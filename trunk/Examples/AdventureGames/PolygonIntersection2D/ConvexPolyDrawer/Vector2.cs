using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch.ConvexPolyDrawer
{
    public class Vector2
    {
        public static Point Subtract(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static void Normalize(Point vector, out float edgeLength, out Point normalVector)
        {
            edgeLength = Length(vector);
            normalVector = new Point(vector.X / edgeLength, vector.Y / edgeLength);
        }

        public static float Length(Point vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        internal static float DotProduct(Point a, Point b)
        {
            return (a.X * b.X + a.Y * b.Y);
        }
    }
}
