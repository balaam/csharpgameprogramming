using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Engine.PathFinding
{
    /// <summary>
    /// A polygon that is expected to be user edited and always remain in a convex state.
    /// </summary>
    public class ConvexPolygon
    {
        List<Point> _vertices = new List<Point>();
        List<IndexedEdge> _indexedEdges = new List<IndexedEdge>();

        #region Accessors
        /// <summary>
        /// Vertices representing the polygon in clockwise order
        /// </summary>
        public List<Point> Vertices
        {
            get { return _vertices; }
        }

        public List<IndexedEdge> Edges
        {
            get { return _indexedEdges; }
        }
        #endregion


        /// <summary>
        /// Create a square convex polygon of 100x100 at <paramref name="position"/>
        /// </summary>
        /// <param name="position">Position to place the polygon.</param>
        public ConvexPolygon(Point position) : this(position, 100, 100) { }

        /// <summary>
        /// Create a rectangle convex polygon at <paramref name="position"/> <paramref name="width"/>xparamref name="height"/>
        /// </summary>
        /// <param name="position"></param>
        /// <param name="halfWidth"></param>
        /// <param name="halfHeight"></param>
        public ConvexPolygon(Point position, float halfWidth, float halfHeight)
        {
            // Position the verts in a rectangle shape, in clockwise order.
            _vertices.Add(new Point(position.X - halfWidth, position.Y + halfHeight));
            _vertices.Add(new Point(position.X + halfWidth, position.Y + halfHeight));
            _vertices.Add(new Point(position.X + halfWidth, position.Y - halfHeight));
            _vertices.Add(new Point(position.X - halfWidth, position.Y - halfHeight));

            // Build the edges between the verts.
            GenerateEdges();
        }

        /// <summary>
        /// Uses the vertex data to build edges between the vertices.
        /// </summary>
        private void GenerateEdges()
        {
            _indexedEdges.Clear();
            for (int i = 1; i < _vertices.Count; i++)
            {
                _indexedEdges.Add(new IndexedEdge(i - 1, i));
            }
            // Add the final edge (n-1)->(0) ti close the polygon loop.
            _indexedEdges.Add(new IndexedEdge(_vertices.Count - 1, 0));
        }

        /// <summary>
        /// Determines if the given a point is inside the polygon.
        /// </summary>
        /// <param name="point">The point to perform the intersect on.</param>
        /// <returns>Is the point in the polygon?</returns>
        public bool Intersects(Point point)
        {
            return Intersects(point.X, point.Y);
        }

        /// <summary>
        /// Determines if the given a point is inside the polygon.
        /// Taken from http://local.wasp.uwa.edu.au/~pbourke/geometry/insidepoly/
        /// </summary>
        /// <param name="x">X position of point</param>
        /// <param name="y">Y position of point</param>
        /// <returns>Is the point in the polygon?</returns>
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

        /// <summary>
        /// Test if the polygon is currently in a concave state.
        /// </summary>
        /// <returns>True if concave.</returns>
        bool IsConcave()
        {
            int positive = 0;
            int negative = 0;
            int length = _vertices.Count;

            for (int i = 0; i < length; i++)
            {
                Point p0 = _vertices[i];
                Point p1 = _vertices[(i + 1) % length];
                Point p2 = _vertices[(i + 2) % length];

                // Subtract to get vectors
                Point v0 = new Point(p0.X - p1.X, p0.Y - p1.Y);
                Point v1 = new Point(p1.X - p2.X, p1.Y - p2.Y); 
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


        /// <summary>
        /// Attempts to update the position of one of the polygons vertices.
        /// The position won't be updated if it would cause the polygon to become concave.
        /// </summary>
        /// <param name="vertexIndex">The index to the vertex to be updated.</param>
        /// <param name="vertexPosition">The new position to try and update the vertex with.</param>
        public void TryToUpdateVertPosition(int vertexIndex, Point vertexPosition)
        {
            Point oldPoisition = _vertices[vertexIndex];
            _vertices[vertexIndex] = vertexPosition;

            if (IsConcave())
            {
                _vertices[vertexIndex] = oldPoisition;
            }

        }

        /// <summary>
        /// Attmept to add a vertex to the polygon.
        /// If the vertex would form a concave polygon then it is not added.
        /// </summary>
        /// <param name="vertexPosition">The vertex to try to add.</param>
        public void TryToAddVertex(Point vertexPosition)
        {
            IndexedEdge edgeIndex = GetClosestEdge(vertexPosition);
            _vertices.Insert(edgeIndex.End, vertexPosition);

            if (IsConcave())
            {
                _vertices.RemoveAt(edgeIndex.End);
                return;
            }

            GenerateEdges();
        }


        /// <summary>
        /// Given a 2d point in the world return the polygon's indexed edge that is closest to that point.
        /// </summary>
        /// <param name="vertexPosition">Position to compare.</param>
        /// <param name="minDistance">The distance the point is from the closest edge.</param>
        /// <returns>The closest edge</returns>
        public IndexedEdge GetClosestEdge(Point vertexPosition, out float minDistance)
        {
            minDistance = float.MaxValue;
            IndexedEdge current = new IndexedEdge();
            foreach (IndexedEdge edge in _indexedEdges)
            {
                Point start = _vertices[edge.Start];
                Point end = _vertices[edge.End];
                float distance = (float)LineSegment.GetDistance(start, end, vertexPosition);
                if (distance < minDistance)
                {
                    current = edge;
                    minDistance = distance;
                }
            }
            return current;
        }
        /// <summary>
        /// Given a 2d point in the world return the polygon's indexed edge that is closest to that point.
        /// </summary>
        /// <param name="vertexPosition">Position to compare.</param>
        /// <returns>The closest edge</returns>
        public IndexedEdge GetClosestEdge(Point vertexPosition)
        {
            float minDistance;
            return GetClosestEdge(vertexPosition, out minDistance);
        }

        /// <summary>
        /// Get the distance from the point to the closest edge.
        /// </summary>
        /// <param name="vertexPosition">The point to compare.</param>
        /// <returns>THe minimum distance of the point to the closest polygon edge.</returns>
        internal float GetClosestEdgeDistance(Point vertexPosition)
        {
            float minDistance;
            GetClosestEdge(vertexPosition, out minDistance);
            return minDistance;
        }

        /// <summary>
        /// Works out the centroid (aka bary centre, geometric centre) for the polygon.
        /// Made by taking the average of the vertices.
        /// </summary>
        /// <returns>The centroid position.</returns>
        public Point CalculateCentroid()
        {
            Point centroid = new Point(0, 0);
            foreach (Point point in Vertices)
            {
                centroid.X += point.X;
                centroid.Y += point.Y;
            }
            centroid.X /= _vertices.Count;
            centroid.Y /= _vertices.Count;
            return centroid;
        }
    }
}
