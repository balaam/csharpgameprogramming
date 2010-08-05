using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.PathFinding
{
    /// <summary>
    /// A navigation mesh is a simple list of convex polygons and the connections between them.
    /// </summary>
    public class NavMesh
    {
        List<ConvexPolygon> _polygonList = new List<ConvexPolygon>();
        List<PolygonLink> _links = new List<PolygonLink>();

        public List<ConvexPolygon> PolygonList
        {
            get { return _polygonList; }
        }

        public List<PolygonLink> Links
        {
            get { return _links; }
        }

        public void AddPolygon(ConvexPolygon polygon)
        {
            _polygonList.Add(polygon); 
        }

        public void AddLink(PolygonLink link)
        {
            _links.Add(link);
        }

        public ConvexPolygon FindNearestPolygon(Point mousePos)
        {
            ConvexPolygon nearestPolygon = null;
            float minDistance = float.MaxValue;
            foreach (ConvexPolygon poly in _polygonList)
            {
                float distance = poly.GetClosestEdgeDistance(mousePos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPolygon = poly;
                }
            }
            return nearestPolygon;
        }
    }
}
