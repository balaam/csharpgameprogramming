using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch
{
    struct Triangle
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }

        public Point CalculateCentroid()
        {
            float x = (A.X + B.X + C.X) / 3;
            float y = (A.Y + B.Y + C.Y) / 3;
            return new Point(x, y);
        }
    }
}
