using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch
{
    class PolyVertex
    {
        public Tween Tween { get; set; }
        public Point Point { get; set; }

        public PolyVertex(Point point, Tween tween)
        {
            Point = point;
            Tween = tween;
        }
    }
}
