using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Tao.OpenGl;
namespace WalkablePolygonCodeSketch.PathTest
{
    /// <summary>
    /// Create a path of nodes from a nav mesh to give to the next star to work out paths.
    /// </summary>
    class SetupStatePathCreation : IGameObject
    {
        Input _input;
        PathData _pathData;
        StateSystem _system;
        PolygonPlotter _plotter = new PolygonPlotter();

        public SetupStatePathCreation(Input input, StateSystem system, PathData pathData)
        {
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            _input = input;
            _system = system;
            _pathData = pathData;
        }

        public void Update(double elapsedTime)
        {
            _plotter.Update(elapsedTime, _input.Mouse.Position);

            if (_input.Mouse.LeftPressed)
            {
                Polygon polygon = _plotter.AddPoint(_input.Mouse.Position);
                if (polygon != null)
                {
                    _pathData.NavMesh.Polygon = polygon;
                    _system.ChangeState("test_path");
                }
            }
        }

        public void Render()
        {
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Point mousePosition = _input.Mouse.Position;
            PrimitiveDrawer.DrawCrosshair(mousePosition.X, mousePosition.Y);

            _pathData.NavMesh.Render();

            _plotter.Render();
        }

        public void Activated()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }
    }
}
