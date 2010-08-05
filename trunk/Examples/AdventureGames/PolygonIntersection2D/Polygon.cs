using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch
{
    class Polygon
    {
        private List<Point> _vertexList = new List<Point>();

        public List<Point> VertexList
        {
            get
            {
                return _vertexList;
            }
        }

        public Polygon(List<PolyVertex> vertexList)
        {
            vertexList.ForEach(x => _vertexList.Add(x.Point));
        }

        public IEnumerator<Point> GetEnumerator() 
        {
            foreach (Point point in _vertexList) 
            {
                yield return point;
            }
        }


        // http://local.wasp.uwa.edu.au/~pbourke/geometry/insidepoly/
        public bool Intersects(float x, float y)
        {
            int npol = _vertexList.Count;
            int i, j = 0;
            bool c = false;
            for (i = 0, j = npol - 1; i < npol; j = i++)
            {
                if ((((_vertexList[i].Y <= y) && (y < _vertexList[j].Y)) ||
                     ((_vertexList[j].Y <= y) && (y < _vertexList[i].Y))) &&
                    (x < (_vertexList[j].X - _vertexList[i].X) * (y - _vertexList[i].Y) / (_vertexList[j].Y - _vertexList[i].Y) + _vertexList[i].X))
                
                    c = !c;
            }
            return c;
        }


    }
}
