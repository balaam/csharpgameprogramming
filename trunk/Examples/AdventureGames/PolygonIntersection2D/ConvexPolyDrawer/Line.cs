using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch.ConvexPolyDrawer
{
    class LineSegment
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

        internal Point GetMiddle()
        {
            return GetMiddle(Start, End);
        }
    }
}
