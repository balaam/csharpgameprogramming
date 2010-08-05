using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WalkablePolygonCodeSketch.PathTest
{
    class PathData
    {
        public NavMesh2d NavMesh {get; set;}

        public PathData()
        {
            NavMesh = new NavMesh2d();
        }
    }
}
