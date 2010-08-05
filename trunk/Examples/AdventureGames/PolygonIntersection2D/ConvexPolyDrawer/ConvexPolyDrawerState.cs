using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Tao.OpenGl;



namespace WalkablePolygonCodeSketch.ConvexPolyDrawer
{

    class ConvexPolyDrawerState : IGameObject
    {
        enum EditState
        {
            ConnectPressed,
            VertexPressed,
            Default,
        }



        Input _input;
        List<ConvexPolyForm> _convexPolyForm = new List<ConvexPolyForm>();
        ConvexPolyForm _selectedPoly = null;
        int _selectedVert = -1;


        EditState _editState = EditState.Default;
        Point _connectStart;
        List<Tuple<ConvexPolyForm, EdgeIndex>> _connectIntersectedEdges = new List<Tuple<ConvexPolyForm, EdgeIndex>>();
        List<Connection> _connections = new List<Connection>();

        StateSystem _stateSystem;
        PathFinder _pathFinder;

        public ConvexPolyDrawerState(Input input, PathFinder pathFinder, StateSystem system)
        {
            _input = input;
            _stateSystem = system;
            _pathFinder = pathFinder;
        }
        public void Activated()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPointSize(10);
        }

        public void Update(double elapsedTime)
        {
            Point mousePos = _input.Mouse.Position;

            FindNearestPolygon(mousePos);
            FindSelectedVertex(mousePos);

            if (_input.Keyboard.IsKeyPressed(System.Windows.Forms.Keys.A))
            {
                _convexPolyForm.Add(new ConvexPolyForm(mousePos));
            }

            if (_input.Keyboard.IsKeyPressed(System.Windows.Forms.Keys.I))
            {
                if (_selectedPoly != null)
                {
                    _selectedPoly.TryAddVert(mousePos);
                }
            }

            if (_input.Keyboard.IsKeyPressed(System.Windows.Forms.Keys.C))
            {
                _editState = EditState.ConnectPressed;
                _connectStart = mousePos;
            }

            if (_input.Keyboard.IsKeyPressed(System.Windows.Forms.Keys.F))
            {
                _pathFinder.Init(_convexPolyForm, _connections);
                _stateSystem.ChangeState("pathfinder");
            }

            if (_input.Mouse.LeftPressed && _editState == EditState.ConnectPressed)
            {
                CheckForConnection();
                _editState = EditState.Default;
            }

            if (_editState == EditState.ConnectPressed)
            {
                FindIntersectedEdges(_connectStart, mousePos);
            }

            if (_input.Mouse.LeftHeld && _selectedVert != -1)
            {
                _editState = EditState.VertexPressed;
                _selectedPoly.TryUpdateVertPosition(_selectedVert, mousePos);
            }
            else
            {
                if (_editState == EditState.VertexPressed)
                {
                    _editState = EditState.Default;
                }
 
            }

           
        }

        private void CheckForConnection()
        {
            if (_connectIntersectedEdges.Count != 2)
            {
                // Connections should be between two edges.
                return;
            }

            var start = _connectIntersectedEdges[0];
            var end = _connectIntersectedEdges[1];

            ConvexPolyForm startPoly = start.Item1;
            ConvexPolyForm endPoly = end.Item1;

            if (startPoly == endPoly)
            {
                // Edges should be between two different polygons
                return;
            }

            // ok it's a valid connection.
            Connection connection = new Connection(startPoly, start.Item2, endPoly, end.Item2);
            _connections.Add(connection);


        }

        private void FindIntersectedEdges(Point start, Point end)
        {
       
            foreach (ConvexPolyForm poly in _convexPolyForm)
            {
                foreach(EdgeIndex edgeIndex in poly.Edges)
                {
                    Point localStart = poly.Vertices[edgeIndex.Start];
                    Point localEnd = poly.Vertices[edgeIndex.End];
                    Point collisionPoint;
                    bool collision = EdgeIndex.Intersects(start, end, localStart, localEnd, out collisionPoint);
                    if (collision)
                    {
                        _connectIntersectedEdges.Add(new Tuple<ConvexPolyForm, EdgeIndex>(poly, edgeIndex));
                    }
                }
            }
            
        }

        private void FindSelectedVertex(Point mousePos)
        {
            if (_editState != EditState.Default)
            {
                return;
            }
            if (_selectedPoly == null)
            {
                _selectedVert = -1;
                return;
            }

            for (int i = 0; i < _selectedPoly.Vertices.Count; i++)
            {
                Circle circle = new Circle(_selectedPoly.Vertices[i].X, _selectedPoly.Vertices[i].Y, 15);
                if (circle.Intersects(mousePos))
                {
                    _selectedVert = i;
                    return;
                }
            }
            _selectedVert = -1;
        }

        private void FindNearestPolygon(Point mousePos)
        {
            float minDistance = float.MaxValue;
            foreach (ConvexPolyForm poly in _convexPolyForm)
            {
                float distance = poly.GetClosestEdgeDistance(mousePos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    _selectedPoly = poly;
                }
            }
        }

        public void Render()
        {
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _convexPolyForm.ForEach(x => Render(x));
            RenderConnections();
        }

        private void RenderConnections()
        {
           // Render the connections between the edges
            foreach (Connection connection in _connections)
            {
                Point firstStart = connection.StartPoly.Vertices[connection.StartEdgeIndex.Start];
                Point firstEnd  = connection.StartPoly.Vertices[connection.StartEdgeIndex.End];
                float length1 = EdgeIndex.Length(firstStart, firstEnd);

                Point secondStart = connection.EndPoly.Vertices[connection.EndEdgeIndex.Start];
                Point secondEnd = connection.EndPoly.Vertices[connection.EndEdgeIndex.End];
                float length2 = EdgeIndex.Length(secondStart, secondEnd);

                if (length1 <= length2)
                {
                    Point point = LineSegment.GetMiddle(firstStart, firstEnd);
                    PrimitiveDrawer.DrawFilledCircle(point, 15, new Color(0, 0, 1, 1));
                }
                else
                {
                    Point point = LineSegment.GetMiddle(secondStart, secondEnd);
                    PrimitiveDrawer.DrawFilledCircle(point, 15, new Color(0, 0, 1, 1));
                }
            }
        }

        private void Render(ConvexPolyForm poly)
        {
            if (poly == _selectedPoly)
            {
                GLUtil.SetColor(new Color(0, 0, 0, 1));
            }
            else
            {
                GLUtil.SetColor(new Color(0.1f, 0.1f, 0.1f, 0.5f));
            }
            PolyDrawer.RenderPoly(poly);

            RenderNormals(poly);
            RenderCentroid(poly);
            

            if (poly == _selectedPoly)
            {
                RenderNearestEdge(poly);
                RenderVerts(poly);
            }

            if (_editState == EditState.ConnectPressed)
            {
                GLUtil.SetColor(new Color(0, 0,0,1));
                PrimitiveDrawer.DrawLine2d(_connectStart, _input.Mouse.Position);
                RenderCrossedEdges();
            }
        }

        private void RenderCrossedEdges()
        {
            foreach(var tuple in _connectIntersectedEdges)
            {
                ConvexPolyForm poly = tuple.Item1;
                EdgeIndex edge = tuple.Item2;

                Point start = poly.Vertices[edge.Start];
                Point end = poly.Vertices[edge.End];

                GLUtil.SetColor(new Color(0, 0, 1, 1));
                PrimitiveDrawer.DrawLine2d(start, end);
            }
        }

        private void RenderCentroid(ConvexPolyForm poly)
        {
           PrimitiveDrawer.DrawFilledCircle(poly.GetCentroid(), 15, new Color(1,1, 0,1 ));
        }

        private void RenderVerts(ConvexPolyForm poly)
        {

            for (int i = 0; i < poly.Vertices.Count; i++)
            {
                Point p = poly.Vertices[i];
                PrimitiveDrawer.DrawCircle(new Circle(p.X, p.Y, 16), new Color(0, 0, 0, 1));

                if (i == _selectedVert && _editState == EditState.VertexPressed)
                {

                    PrimitiveDrawer.DrawFilledCircle(p, 15, new Color(0, 1, 1, 1));
                }
                else
                {
                    PrimitiveDrawer.DrawFilledCircle(p, 15, new Color(1, 1, 1, 1));
                }
            }

        }

        private void RenderNormals(ConvexPolyForm poly)
        {
            GLUtil.SetColor(new Color(0, 1, 0, 1));
    
            foreach (EdgeIndex edge in poly.Edges)
            {
                Point start = poly.Vertices[edge.Start];
                Point end = poly.Vertices[edge.End];
                Point middle = LineSegment.GetMiddle(start, end);
                Point normal = EdgeIndex.GetLineNormal(start, end);
                Gl.glBegin(Gl.GL_LINES);
                {
                    GLUtil.DrawPointVertex(middle);
                    GLUtil.DrawPointVertex(new Point(middle.X + (normal.X*50), middle.Y + (normal.Y*50)));
                }
                Gl.glEnd();
            }
            
          

        }

        private void RenderNearestEdge(ConvexPolyForm poly)
        {
            EdgeIndex edge = poly.GetClosestEdge(_input.Mouse.Position);
            GLUtil.SetColor(new Color(1, 0, 0, 1));
            Gl.glBegin(Gl.GL_LINE_STRIP);
            {
                GLUtil.DrawPointVertex(poly.Vertices[edge.Start]);
                GLUtil.DrawPointVertex(poly.Vertices[edge.End]);
            }
            Gl.glEnd();

            // Render closest point
            Point p = EdgeIndex.GetClosestPoint(poly.Vertices[edge.Start], poly.Vertices[edge.End], _input.Mouse.Position);
            Gl.glBegin(Gl.GL_POINTS);
            {
                GLUtil.DrawPointVertex(p);
            }
            Gl.glEnd();
        }
    }
}
