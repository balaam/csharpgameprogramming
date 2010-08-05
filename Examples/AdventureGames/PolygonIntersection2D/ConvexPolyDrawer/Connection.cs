using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch.ConvexPolyDrawer
{

    struct Connection
    {
        public ConvexPolyForm StartPoly;
        public ConvexPolyForm EndPoly;
        public EdgeIndex StartEdgeIndex;
        public EdgeIndex EndEdgeIndex;

        public Connection(ConvexPolyForm startPoly, EdgeIndex startEdge, ConvexPolyForm endPoly, EdgeIndex endEdge)
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
            float length1 = EdgeIndex.Length(firstStart, firstEnd);

            Point secondStart = EndPoly.Vertices[EndEdgeIndex.Start];
            Point secondEnd = EndPoly.Vertices[EndEdgeIndex.End];
            float length2 = EdgeIndex.Length(secondStart, secondEnd);

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
