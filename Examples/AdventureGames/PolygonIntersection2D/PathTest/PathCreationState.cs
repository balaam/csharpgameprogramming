using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;

using Tao.OpenGl;

namespace WalkablePolygonCodeSketch.PathTest
{
    class PathCreationState : IGameObject
    {
        Input _input;
        PathData _pathData;

        NavNode _start;
        NavNode _end;

        AStar<NavNode> _astar = new AStar<NavNode>();
        Font _font;
        Renderer _renderer = new Renderer();
   

        class NodeUI
        {
            public NodeUI(NavNode node)
            {
                Node = node;
                Circle = new HighlightCircle(node.Position);
            }
            public void Update(double elapsedTime, Point position)
            {
                Circle.Update(elapsedTime, position);
            }

            public void Render()
            {
                Circle.Render();
            }

            public bool HasFocus()
            {
                return Circle.Focus;
            }
            public NavNode Node { get; set; }
            HighlightCircle Circle { get; set; }
        }

        List<NodeUI> _nodeUI = new List<NodeUI>();

        public PathCreationState(Input input, PathData pathData, Engine.Font numberFont)
        {
            _input = input;
            _pathData = pathData;
            _font = numberFont;

        }

        public void Activated()
        {
            _pathData.NavMesh.NodeList.ForEach(x => _nodeUI.Add(new NodeUI(x)));
        }

        public void Update(double elapsedTime)
        {
            _nodeUI.ForEach(x => x.Update(elapsedTime, _input.Mouse.Position));

            if (_input.Mouse.LeftPressed)
            {
                NodeUI node = _nodeUI.Find(x => x.HasFocus());

                if (node != null)
                {
                    if (_start == null)
                    {
                        _start = node.Node;
                    }
                    else if (_end == null)
                    {
                        _end = node.Node;
                        _astar.FindPath(_start, _end);
                        _astar.Path.Reverse();
                    }
                    else
                    {
                        // Time to reset.
                    }
                }
            }
        }

        private List<Edge> CreateEdgeList(List<NavNode> list)
        {
            List<Edge> edgeList = new List<Edge>();
            NavNode _lastNode = null;
            foreach (NavNode node in list)
            {
                if (_lastNode == null)
                {
                    _lastNode = node;
                    continue;
                }

                var indexedEdge = node.GetNeighboursEdge(_lastNode);

                Point start = _pathData.NavMesh.Polygon.VertexList[indexedEdge.Item1];
                Point end = _pathData.NavMesh.Polygon.VertexList[indexedEdge.Item2];
                edgeList.Add(new Edge(start, end));
                _lastNode = node;
            }
            return edgeList;
        }

        public void Render()
        {
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Point mousePosition = _input.Mouse.Position;
            PrimitiveDrawer.DrawCrosshair(mousePosition.X, mousePosition.Y);
            _pathData.NavMesh.Render();
            _nodeUI.ForEach(x => x.Render());

            if (_start != null)
            {
                PrimitiveDrawer.DrawSquare(_start.Position.X, _start.Position.Y, 20, new Color(0, 0, 0, 1));
            }

            if (_end != null)
            {
                PrimitiveDrawer.DrawSquare(_end.Position.X, _end.Position.Y, 20, new Color(0.1f, 0.1f, 0.1f, 1));
            }

            if (_astar.Path.Count != 0)
            {
                DrawPath(_astar.Path);
            }
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            _renderer.Render();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            _textBuffer.Clear();
        }

        Dictionary<Point, Point> _textBuffer = new Dictionary<Point, Point>();
        private void DrawText(float x, float y, string text)
        {
            Point p = new Point(x, y);

            if (_textBuffer.ContainsKey(p))
            {

                y = _textBuffer[p].Y - 15;

            }

            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            _renderer.DrawText(x, y, text, _font);
            _textBuffer[p] = new Point(x, y);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            // Gl.glDisable(Gl.GL_BLEND);
        }

        private void DrawPath(List<NavNode> list)
        {
            GLUtil.SetColor(new Color(0, 0, 0, 1));
            Gl.glBegin(Gl.GL_LINE_STRIP);
            {
                foreach (NavNode node in list)
                {

                    GLUtil.DrawPointVertex(node.Position);

                }
            }
            Gl.glEnd();
        }


    }
}