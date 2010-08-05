using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Tao.OpenGl;

namespace WalkablePolygonCodeSketch
{
    class NavMeshCreateState : IGameObject
    {
        Input _input;
        PolygonPlotter _plotter = new PolygonPlotter();
        NavMesh2d _navMesh = new NavMesh2d();

        public NavMeshCreateState(Input input)
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
                    _navMesh.Polygon = polygon;
                }
            }
        }

        public void Render()
        {
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Point mousePosition = _input.Mouse.Position;
            PrimitiveDrawer.DrawCrosshair(mousePosition.X, mousePosition.Y);

            _navMesh.Render();
            _navMesh.DrawNodePaths(new Color(0, 1, 0, 1));

            _plotter.Render();
        }

        public void Activated()
        {

        }
    }
}
