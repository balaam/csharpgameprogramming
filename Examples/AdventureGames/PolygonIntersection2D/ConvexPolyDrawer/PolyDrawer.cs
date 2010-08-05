using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Engine;

namespace WalkablePolygonCodeSketch.ConvexPolyDrawer
{
    class PolyDrawer
    {
        internal static void RenderPoly(ConvexPolyForm poly)
        {
            Gl.glBegin(Gl.GL_LINE_STRIP);
            {
                foreach (Point p in poly.Vertices)
                {
                    GLUtil.DrawPointVertex(p);
                }
                GLUtil.DrawPointVertex(poly.Vertices.First());
            }
            Gl.glEnd();
        }
    }
}
