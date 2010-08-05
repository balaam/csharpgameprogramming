using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Tao.OpenGl;
using WalkablePolygonCodeSketch.ConvexPolyDrawer; // for line normals

namespace WalkablePolygonCodeSketch
{
    class PrimitiveDrawer
    {
        public static void DrawCrosshair(double x, double y)
        {
            DrawCrosshair(x, y, 15, new Color(1, 0, 0, 1));
        }

        public static void DrawCrosshair(double x, double y, double dimension, Color color)
        {
            double halfDimension = dimension / 2;
            GLUtil.SetColor(color);
            Gl.glBegin(Gl.GL_LINES);
            {
                // Left to right
                Gl.glVertex2d(x - halfDimension, y);
                Gl.glVertex2d(x + halfDimension, y);

                // Top to bottom
                Gl.glVertex2d(x, y + halfDimension);
                Gl.glVertex2d(x, y - halfDimension);
            }
            Gl.glEnd();
        }

        public static void DrawDashedCircle(Circle circle, Color color)
        {
            Gl.glPushAttrib(Gl.GL_ENABLE_BIT);
            {
                Gl.glLineStipple(1, 0x00FF);
                Gl.glEnable(Gl.GL_LINE_STIPPLE);
                DrawCircle(circle, color);
            }
            Gl.glPopAttrib();
        }

        public static void DrawCircle(Circle circle, Color color)
        {
         
            GLUtil.SetColor(color);
            Gl.glBegin(Gl.GL_LINE_LOOP);
            PlotCircleVertices(circle.X, circle.Y, circle.Radius);
            Gl.glEnd();
        }

        private static void PlotCircleVertices(double x, double y, double radius)
        {
            double k_segments = 16.0;
            double k_increment = 2.0 * Math.PI / k_segments;
            double theta = 0.0f;
            for (int i = 0; i < k_segments; i++)
            {
                double plotX = x + radius * Math.Cos(theta);
                double plotY = y + radius * Math.Sin(theta);
                Gl.glVertex2d(plotX, plotY);
                theta += k_increment;
            }
        }

        internal static void DrawFilledCircle(Point point, double radius, Color color)
        {
            GLUtil.SetColor(color);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            {
                Gl.glVertex2d(point.X, point.Y);
                PlotCircleVertices(point.X, point.Y, radius);
                Gl.glVertex2d(point.X + radius * Math.Cos(0), point.Y + radius * Math.Sin(0));
            }
            Gl.glEnd();
        }
        
        public static void DrawSquare(double x, double y, double dimension, Color color)
        {
            double halfDimension = dimension / 2;
            GLUtil.SetColor(color);
            Gl.glBegin(Gl.GL_LINES);
            {
                // Top left to top right
                Gl.glVertex2d(x - halfDimension, y + halfDimension);
                Gl.glVertex2d(x + halfDimension, y + halfDimension);

                // Top right to bottom right
                Gl.glVertex2d(x + halfDimension, y + halfDimension);
                Gl.glVertex2d(x + halfDimension, y - halfDimension);

                // Bottom right to bottom left
                Gl.glVertex2d(x + halfDimension, y - halfDimension);
                Gl.glVertex2d(x - halfDimension, y - halfDimension);

                // Bottom left to top left
                Gl.glVertex2d(x - halfDimension, y - halfDimension);
                Gl.glVertex2d(x - halfDimension, y + halfDimension);
            }
            Gl.glEnd();
        }

        public static void DrawPolygon(Polygon polygon, Color color)
        {
            GLUtil.SetColor(color);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            {
                foreach (Point p in polygon)
                {
                    Gl.glVertex2d(p.X, p.Y);
                }
            }
            Point point = polygon.VertexList.First();
            Gl.glVertex2d(point.X, point.Y);
            Gl.glEnd();
        }

        public static void DrawFilledPolygon(Polygon polygon, Color color)
        {
            Triangulator tr = new Triangulator(polygon.VertexList.ToArray());
            int[] indices = tr.Triangulate();
            

            GLUtil.SetColor(color);
            Gl.glBegin(Gl.GL_TRIANGLES);
            {
                foreach (int index in indices)
                {
                    Point p = polygon.VertexList[index];
                    Gl.glVertex2d(p.X, p.Y);
                }
            }
            Gl.glEnd();
        }

        internal static void DrawLine2d(Point start, Point end)
        {
            Gl.glBegin(Gl.GL_LINES);
            {
                Gl.glVertex2d(start.X, start.Y);
                Gl.glVertex2d(end.X, end.Y);
            }
            Gl.glEnd();
        }

        internal static void DrawArrow2d(Point start, Point end)
        {
            Point normal = EdgeIndex.GetLineNormal(start, end);
            Point direction = Vector2.Subtract(end, start);
            float length = 0;
            Vector2.Normalize(direction, out length, out direction);
            // this length 20 should probably be a percentage of the line.
            direction.X *= 20;
            direction.Y *= 20;
            Point arrowHeadEnd = Vector2.Subtract(direction, end);

            Point arrowHead1 = new Point(arrowHeadEnd.X + (normal.X * 20), arrowHeadEnd.Y + (normal.Y * 20));
            Point arrowHead2 = new Point(arrowHeadEnd.X + (normal.X * -20), arrowHeadEnd.Y + (normal.Y * -20));

            DrawLine2d(start, end);
            DrawLine2d(arrowHead1, end);
            DrawLine2d(arrowHead2, end);
        }

   
    }
}
