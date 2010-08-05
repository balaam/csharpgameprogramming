using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Engine;

namespace WalkablePolygonCodeSketch
{
    class IndexedTriangleCollection :  IEnumerable
    {
        int[] _indices;
        List<Point> _points;

        public List<Point> Vertices
        {
            set
            {
                _points = value;
            }
        }

        public int[] Indices
        {
            get
            {
                return _indices;
            }
            set
            {
                _indices = value;
            }

        }


        public IEnumerator<Triangle> GetEnumerator()
        {
            for (int i = 0; i < _indices.Length; i += 3)
            {
                Triangle triangle = new Triangle();
                triangle.A = _points[_indices[i]];
                triangle.B = _points[_indices[i + 1]];
                triangle.C = _points[_indices[i + 2]];
                yield return triangle;
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
