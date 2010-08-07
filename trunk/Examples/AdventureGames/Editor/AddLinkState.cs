using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Engine.PathFinding;

namespace Editor
{
    class AddLinkState : IGameObject
    {
        struct PolygonEdgePair
        {
            public ConvexPolygon Polygon;
            public IndexedEdge Edge;
            public PolygonEdgePair(ConvexPolygon polygon, IndexedEdge edge)
            {
                Polygon = polygon;
                Edge = edge;
            }
        }
   
        Input _input;
        bool _haveStartPoint = false;
        Point _startPoint = new Point();
        NavMesh _navMesh;
        // Allows intersected edges to be interactively shown.
        List<PolygonEdgePair> _intersectedEdges = new List<PolygonEdgePair>();

        public AddLinkState(Input input, NavMesh navMesh)
        {
            _input = input;
            _navMesh = navMesh;
        }

        public void Activated()
        {
            _haveStartPoint = false;
        }

        public void Update(double elapsedTime)
        {
            if (_haveStartPoint)
            {
                _intersectedEdges = FindIntersectedEdges(_startPoint, _input.Mouse.Position);
            }

            if (_input.Mouse.LeftPressed)
            {
                if (_haveStartPoint)
                {
                    _haveStartPoint = false;
                    TryAddLink();
                }
                else
                {
                    _haveStartPoint = true;
                    _startPoint = _input.Mouse.Position;
                }
            }
        }

        private void TryAddLink()
        {
            // A link be between exactly two polygons.
            if (_intersectedEdges.Count != 2)
            {
                return;
            }

            // A polygon can't connect with itself
            if (_intersectedEdges[0].Polygon == _intersectedEdges[1].Polygon)
            {
                return;
            }
              
            // Can make a connection.
            PolygonLink polyLink = new PolygonLink(
                    _intersectedEdges[0].Polygon,
                    _intersectedEdges[0].Edge,
                    _intersectedEdges[1].Polygon,
                    _intersectedEdges[1].Edge);
            _navMesh.AddLink(polyLink);
            
         
        }



        private List<PolygonEdgePair> FindIntersectedEdges(Point start, Point end)
        {

            List<PolygonEdgePair> intersectedEdges = new List<PolygonEdgePair>();
            foreach (ConvexPolygon polygon in _navMesh.PolygonList)
            {
                foreach (IndexedEdge edgeIndex in polygon.Edges)
                {
                    Point localStart = polygon.Vertices[edgeIndex.Start];
                    Point localEnd = polygon.Vertices[edgeIndex.End];
                    Point collisionPoint;
                    bool collision = LineSegment.Intersects(start, end, localStart, localEnd, out collisionPoint);
                    if (collision)
                    {
                        intersectedEdges.Add(new PolygonEdgePair(polygon, edgeIndex));
                    }
                }
            }
            return intersectedEdges;

        }

        public void Render()
        {
            if (_haveStartPoint)
            {
                GLUtil.SetColor(new Color(0.8f, 0.3f, 0.8f, 1));
                GLUtil.DrawLine2d(_startPoint, _input.Mouse.Position);
                RenderIntersectEdges();
            }
            RenderLinks();
        }

        private void RenderLinks()
        {
            foreach (PolygonLink link in _navMesh.Links)
            {
                Point position = link.GetShortestEdge().GetMiddle();
                GLUtil.DrawFilledCircle(position, 10, new Color(0.8f, 0.3f, 0.8f, 1));
            }
        }

        private void RenderIntersectEdges()
        {
            foreach (PolygonEdgePair pair in _intersectedEdges)
            {
                GLUtil.DrawLine2d(pair.Polygon.Vertices[pair.Edge.Start], pair.Polygon.Vertices[pair.Edge.End]);
            }
        }
    }
}
