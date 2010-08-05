using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Tao.OpenGl;

namespace WalkablePolygonCodeSketch.ConvexPolyDrawer
{
    class PathFinderState : IGameObject
    {
        Input _input;
        PathFinder _pathFinder;
        Renderer _renderer = new Renderer();
        Font _font;
        bool _drawingPath = false;
        Point _pathStart;
        Point _pathEnd;
        List<Point> _path = new List<Point>();

        public PathFinderState(Input input, PathFinder pathFinder, Font font)
        {
            _input = input;
            _pathFinder = pathFinder;
            _font = font;
        }

        public void Activated()
        {
           
        }

        public void Update(double elapsedTime)
        {
            if (_input.Mouse.LeftPressed)
            {
                if (_drawingPath == false)
                {
                    _pathStart = _input.Mouse.Position;
                    _drawingPath = true;
                }
                else
                {
                    _drawingPath = false;
                    _pathEnd = _input.Mouse.Position;
                   _path = _pathFinder.GetPath(_pathStart, _pathEnd);
                }
            }
            
        }

        public void Render()
        {
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            _pathFinder.Polygons.ForEach(x => PolyDrawer.RenderPoly(x));

            

            if (_drawingPath)
            {
                PrimitiveDrawer.DrawFilledCircle(_pathStart, 15, new Color(0, 0, 0, 1));
            }


                GLUtil.SetColor(new Color(1, 0, 0, 1));
                Gl.glBegin(Gl.GL_LINE_STRIP);
                {

                    foreach (Point p in _path)
                    {
                       GLUtil.DrawPointVertex(p);
                    }
                }
                Gl.glEnd();
            


            _renderer.DrawText(0, 0, "Path Drawing State", _font);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            _renderer.Render();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

        }
    }
}
