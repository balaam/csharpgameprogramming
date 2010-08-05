using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Tao.OpenGl;

namespace WalkablePolygonCodeSketch
{
    class PolygonCreateState : IGameObject
    {
        Input _input;
        PolygonPlotter _plotter = new PolygonPlotter();
        List<Polygon> _polygons = new List<Polygon>();

        public PolygonCreateState(Input input)
        {
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            _input = input;
        }

        public void Update(double elapsedTime)
        {
            _plotter.Update(elapsedTime, _input.Mouse.Position);

            if (_input.Mouse.LeftPressed)
            {
                Polygon polygon = _plotter.AddPoint(_input.Mouse.Position);
                if (polygon != null)
                {
                    _polygons.Add(polygon);
                }
            }
        }

        public void Render()
        {
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Point mousePosition = _input.Mouse.Position;
            PrimitiveDrawer.DrawCrosshair(mousePosition.X, mousePosition.Y);

            foreach (Polygon polygon in _polygons)
            {
                PrimitiveDrawer.DrawPolygon(polygon, new Color(1, 0, 0, 1));
                if (polygon.Intersects(mousePosition.X, mousePosition.Y))
                {
                    PrimitiveDrawer.DrawFilledPolygon(polygon, new Color(1, 1, 0, 0.25f));
                    
                }
                else
                {
                    PrimitiveDrawer.DrawFilledPolygon(polygon, new Color(1, 0, 0, 0.25f));
                }
            }

            _plotter.Render();
        }

        public void Activated()
        {
         
        }
    }
}
