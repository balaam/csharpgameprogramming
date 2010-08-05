using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.PathFinding
{
    /// <summary>
    /// A link between two polygons passing through two edges.
    /// </summary>
    public class PolygonLink
    {
        public ConvexPolygon StartPoly { get; set; }
        public ConvexPolygon EndPoly { get; set; }
        public IndexedEdge StartEdgeIndex { get; set; }
        public IndexedEdge EndEdgeIndex { get; set; }

        public PolygonLink(ConvexPolygon startPoly, IndexedEdge startEdge, ConvexPolygon endPoly, IndexedEdge endEdge)
        {
            StartPoly = startPoly;
            StartEdgeIndex = startEdge;
            EndPoly = endPoly;
            EndEdgeIndex = endEdge;
        }

        public LineSegment GetShortestEdge()
        {
            Point firstStart = StartPoly.Vertices[StartEdgeIndex.Start];
            Point firstEnd = StartPoly.Vertices[StartEdgeIndex.End];
            float length1 = LineSegment.Length(firstStart, firstEnd);

            Point secondStart = EndPoly.Vertices[EndEdgeIndex.Start];
            Point secondEnd = EndPoly.Vertices[EndEdgeIndex.End];
            float length2 = LineSegment.Length(secondStart, secondEnd);

            if (length1 <= length2)
            {
                return new LineSegment(firstStart, firstEnd);
            }
            else
            {
                return new LineSegment(secondStart, secondEnd);
            }
        }
    }
}
