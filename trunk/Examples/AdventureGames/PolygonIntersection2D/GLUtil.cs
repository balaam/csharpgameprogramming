using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Tao.OpenGl;

namespace WalkablePolygonCodeSketch
{
    class GLUtil
    {
        public static void SetColor(Color color)
        {
            Gl.glColor4d(color.Red, color.Green, color.Blue, color.Alpha);

            /*
             * NOTES
             * http://www.easywms.com/easywms/?q=en/node/3602
             * http://softsurfer.com/Archive/algorithm_0104/algorithm_0104B.htm#intersect2D_SegSeg%28%29
Return whether a polygon in 2D is concave or convex
return 0 for incomputables eg: colinear points
CONVEX == 1
CONCAVE == -1
It is assumed that the polygon is simple
(does not intersect itself or have holes)

int Convex(XY *p,int n)
{
int i,j,k;
int flag = 0;
double z;
if (n < 3)
return(0);
for (i=0;i<n;i++) {
j = (i + 1) % n;
k = (i + 2) % n;
z = (p[j].x - p[i].x) * (p[k].y - p[j].y);
z -= (p[j].y - p[i].y) * (p[k].x - p[j].x);
if (z < 0)
flag |= 1;
else if (z > 0)
flag |= 2;
if (flag == 3)
return(CONCAVE);
}
if (flag != 0)
return(CONVEX);
else
return(0);
}
             */
        }

        internal static void DrawPointVertex(Point point)
        {
            Gl.glVertex2d(point.X, point.Y);
        }
    }
}
