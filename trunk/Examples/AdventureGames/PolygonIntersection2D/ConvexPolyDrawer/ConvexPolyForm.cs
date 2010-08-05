using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch.ConvexPolyDrawer
{
    class ConvexPolyForm
    {

        List<Point> _vertices = new List<Point>();
        List<EdgeIndex> _edges = new List<EdgeIndex>();

        public List<Point> Vertices
        {
            get
            {
                return _vertices;
            }
        }

        public List<EdgeIndex> Edges 
        {
            get
            {
                return _edges;
            }
        }

        public Point GetCentroid()
        {
            Point centroid = new Point(0, 0);
            foreach(Point point in Vertices)
            {
                centroid.X += point.X;
                centroid.Y += point.Y;
            }
            centroid.X /= _vertices.Count;
            centroid.Y /= _vertices.Count;
            return centroid;
        }
        

        /// <summary>
        /// A default convex polygon is a square, the verts can then be manipulated.
        /// </summary>
        /// <param name="position"></param>
        public  ConvexPolyForm(Point position)
        {
            float halfWidth = 100;
            float halfHeight = 100;


            _vertices.Add(new Point(position.X - halfWidth, position.Y + halfHeight));
            _vertices.Add(new Point(position.X + halfWidth, position.Y + halfHeight));
            _vertices.Add(new Point(position.X + halfWidth, position.Y - halfHeight));
            _vertices.Add(new Point(position.X - halfWidth, position.Y - halfHeight));

            GenerateEdges();

        }

        private void GenerateEdges()
        {
            _edges.Clear();
            for (int i = 1; i < _vertices.Count; i++)
            {
                _edges.Add(new EdgeIndex(i - 1, i));
            }
            _edges.Add(new EdgeIndex(_vertices.Count - 1, 0));
        }

        public EdgeIndex GetClosestEdge(Point p, out float minDistance)
        {
            minDistance = float.MaxValue;
            EdgeIndex current = new EdgeIndex();
            foreach (EdgeIndex edge in _edges)
            {
                Point start = _vertices[edge.Start];
                Point end = _vertices[edge.End];
                float distance = (float)EdgeIndex.GetDistance(start, end, p);
                if (distance < minDistance)
                {
                    current = edge;
                    minDistance = distance;
                }
            }
            return current;
        }

        public EdgeIndex GetClosestEdge(Point p)
        {
            float minDistance;
            return GetClosestEdge(p, out minDistance);
        }

        internal float GetClosestEdgeDistance(Point p)
        {
            float minDistance;
            GetClosestEdge(p, out minDistance);
            return minDistance;
        }


        // http://local.wasp.uwa.edu.au/~pbourke/geometry/insidepoly/
        public bool Intersects(float x, float y)
        {
            bool intersects = false;
            for (int i = 0, j = _vertices.Count - 1; i < _vertices.Count; j = i++)
            {
                if ((((_vertices[i].Y <= y) && (y < _vertices[j].Y)) ||
                     ((_vertices[j].Y <= y) && (y < _vertices[i].Y))) &&
                    (x < (_vertices[j].X - _vertices[i].X) * (y - _vertices[i].Y) / (_vertices[j].Y - _vertices[i].Y) + _vertices[i].X))

                    intersects = !intersects;
            }
            return intersects;
        }


        bool IsConcave()
        {
            int positive = 0;
            int negative = 0;
            int length = _vertices.Count;

            for (var i = 0; i < length; i++)
            {
                Point p0 = _vertices[i];
                Point p1 = _vertices[(i + 1) % length];
                Point p2 = _vertices[(i + 2) % length];

                Point v0 = Vector2.Subtract(p0, p1);
                Point v1 = Vector2.Subtract(p1, p2); 
                float cross = (v0.X * v1.Y) - (v0.Y * v1.X);


                if (cross < 0)
                {
                    negative++;
                }
                else
                {
                    positive++;
                }
            }

            return (negative != 0 && positive != 0);

        }



        internal void TryUpdateVertPosition(int _selectedVert, Point mousePos)
        {
            Point oldPoisition = _vertices[_selectedVert];
            _vertices[_selectedVert] = mousePos;
            if (IsConcave())
            {
                _vertices[_selectedVert] = oldPoisition;
            }
           
        }

        internal void TryAddVert(Point mousePos)
        {
            EdgeIndex edgeIndex = GetClosestEdge(mousePos);
            _vertices.Insert(edgeIndex.End, mousePos);
            GenerateEdges();
            if (IsConcave())
            {
                _vertices.RemoveAt(edgeIndex.End);
                GenerateEdges();
            }
        }
    }
}
