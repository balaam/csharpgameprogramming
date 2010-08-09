using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;
using Engine.Input;
using Tao.OpenGl;
using Engine.PathFinding;

namespace Editor
{
    class TestWalkState : IGameObject
    {

        Input _input;
        ToolStrip _toolstrip;
        Scene _scene;
        Renderer _renderer = new Renderer();
        List<Point> _path = new List<Point>();

        bool _canWalkMousePosition = false;
        bool _firstPress = true;
        Point _pathStart = new Point();
        PathFinder _pathFinder = new PathFinder();

        public TestWalkState(Input input, ToolStrip toolstrip, Scene scene)
        {
            _input = input;
            _toolstrip = toolstrip;
            _scene = scene;
        }

        public void Activated()
        {
       
        }

        public void Update(double elapsedTime)
        {
            _canWalkMousePosition = _scene.NavMesh.PolygonList.Any(x => x.Intersects(_input.Mouse.Position));
            if (_input.Mouse.LeftPressed && _canWalkMousePosition)
            {
                if (_firstPress)
                {
                    _firstPress = false;
                    _pathStart = _input.Mouse.Position;
                }
                else
                {
                    _path = _pathFinder.GetPath(_pathStart, _input.Mouse.Position, _scene.NavMesh);
                    _firstPress = true;

                }
            }
        }

        public void Render()
        {
            GLUtil.Clear(new Color(0, 0, 0, 1));

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            foreach (Layer layer in _scene.Layers)
            {
                layer.Render(_renderer);
            }
            _renderer.Render();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            if (_canWalkMousePosition)
            {
                GLUtil.DrawFilledCircle(_input.Mouse.Position, 10, new Color(0, 1, 0, 1));
            }
            else
            {
                GLUtil.DrawFilledCircle(_input.Mouse.Position, 10, new Color(1, 0, 0, 1));
            }

            if (_firstPress == false)
            {
                GLUtil.DrawFilledCircle(_pathStart, 10, new Color(1, 1, 0, 1));
            }

            // Draw the basic nav meshes
            GLUtil.SetColor(new Color(1, 0, 0, 1));
            _scene.NavMesh.PolygonList.ForEach(x => GLUtil.RenderPolygon(x));
            GLUtil.SetColor(new Color(1, 1, 1, 1));
            Gl.glLineWidth(4);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            {
                foreach (Point point in _path)
                {

                    GLUtil.DrawPointVertex(point);

                }
            }
            Gl.glEnd();
            Gl.glLineWidth(1);
        }
    }
}
