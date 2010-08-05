using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch
{
    public struct Edge
    {
        public Point Top { get; set; }
        public Point Bottom { get; set; }

        public Edge(Point top, Point bottom)
            : this()
        {
            Top = top;
            Bottom = bottom;
        }

      

  
    }
}
