using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WalkablePolygonCodeSketch
{
    class Circle
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; } 

        public Circle(double x, double y, double radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public bool Intersects(double x, double y)
        {
            double xDiff = x - X;
            double yDiff = y - Y;
            double lengthDiff = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
            return lengthDiff <= Radius;
        }


        internal bool Intersects(Engine.Point mousePosition)
        {
            return Intersects(mousePosition.X, mousePosition.Y);
        }
    }
}
