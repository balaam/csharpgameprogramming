using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Engine;

namespace Editor
{
    class GLUtil
    {
        public static void Clear(Color color)
        {
            Gl.glClearColor(0.5f, 0.5f, 0.5f, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
        }

        public static void SetColor(Color color)
        {
            Gl.glColor4d(color.Red, color.Green, color.Blue, color.Alpha);
        }

        internal static void DrawPointVertex(Point point)
        {
            Gl.glVertex2d(point.X, point.Y);
        }

        internal static void RenderPolygon(Engine.PathFinding.ConvexPolygon polygon)
        {
            Gl.glBegin(Gl.GL_LINE_STRIP);
            {
                foreach (Point p in polygon.Vertices)
                {
                    GLUtil.DrawPointVertex(p);
                }
                GLUtil.DrawPointVertex(polygon.Vertices.First());
            }
            Gl.glEnd();
        }

        internal static void RenderPolygonFilled(Engine.PathFinding.ConvexPolygon polygon)
        {
            Gl.glBegin(Gl.GL_POLYGON);
            {
                foreach (Point p in polygon.Vertices)
                {
                    GLUtil.DrawPointVertex(p);
                }
                GLUtil.DrawPointVertex(polygon.Vertices.First());
            }
            Gl.glEnd();
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

        internal static void DrawCircle(Point point, int radius, Color color)
        {
            Circle circle = new Circle(point.X, point.Y, radius);
            DrawCircle(circle, color);
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

    }
}
