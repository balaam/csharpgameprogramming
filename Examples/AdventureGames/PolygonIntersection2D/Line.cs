using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch
{
    class LineSegment
    {

        public static double LinePointDistance(double x1, double y1, double x2, double y2, double px, double py)
        {
            // Adjust vectors relative to x1,y1
            // x2,y2 becomes relative vector from x1,y1 to end of segment
            x2 -= x1;
            y2 -= y1;
            // px,py becomes relative vector from x1,y1 to test point
            px -= x1;
            py -= y1;
            double dotprod = px * x2 + py * y2;
            // dotprod is the length of the px,py vector
            // projected on the x1,y1=>x2,y2 vector times the
            // length of the x1,y1=>x2,y2 vector
            double projlenSq = dotprod * dotprod / (x2 * x2 + y2 * y2);
            // Distance to line is now the length of the relative point
            // vector minus the length of its projection onto the line
            double lenSq = px * px + py * py - projlenSq;
            if (lenSq < 0)
            {
                lenSq = 0;
            }
            return Math.Sqrt(lenSq);
        }




    }
}
