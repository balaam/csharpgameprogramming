using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Tao.OpenGl;

namespace WalkablePolygonCodeSketch
{
    class NavMesh2d
    {
        Polygon _polygon;
        IndexedTriangleCollection _indexedTriMesh = new IndexedTriangleCollection();
        List<NavNode> _allNodes = new List<NavNode>();
        AStar<NavNode> _aStar = new AStar<NavNode>();

        public List<NavNode> NodeList
        {
            get
            {
                return _allNodes;
            }
        }

        public Polygon Polygon
        {
            get
            {
                return _polygon;
            }
            set
            {
                _polygon = value;
                Triangulator tr = new Triangulator(_polygon.VertexList.ToArray());
                _indexedTriMesh.Vertices = _polygon.VertexList;
                _indexedTriMesh.Indices = tr.Triangulate();
                _allNodes.Clear();
                CollectNodes(_indexedTriMesh.Indices, _allNodes);

         
               // _aStar.FindPath(_allNodes.First(), _allNodes.Last());


            }
        }

        private void CollectNodes(int[] indices, List<NavNode> nodeList)
        {
            for (int i = 0; i < _indexedTriMesh.Indices.Length; i+= 3)
            {
                int aIndex = indices[i];
                int bIndex = indices[i + 1];
                int cIndex = indices[i + 2];

                Triangle triangle = new Triangle();
                triangle.A = _polygon.VertexList[aIndex];
                triangle.B = _polygon.VertexList[bIndex];
                triangle.C = _polygon.VertexList[cIndex];

                NavNode node = new NavNode(triangle.CalculateCentroid());
                node.AddEdge(new Tuple<int, int>(aIndex, bIndex));
                node.AddEdge(new Tuple<int, int>(bIndex, cIndex));
                node.AddEdge(new Tuple<int, int>(cIndex, aIndex));

                nodeList.Add(node);
            }

            // Find adjacent nodes
            foreach (NavNode node in nodeList)
            {
                foreach (NavNode otherNode in nodeList)
                {
                    if (node == otherNode)
                    {
                        continue;
                    }

                    Tuple<int, int> edge = node.FindSharedEdge(otherNode);
                    if (edge != null)
                    {
                        node.AddAdjacentNode(otherNode, edge);
                    }
                }
            }

          
        }

        public void DrawTriangleCentroid(Color color, Triangle triangle)
        {
            Gl.glPointSize(20.0f);
            Gl.glBegin(Gl.GL_POINTS);
            {
                Point p = triangle.CalculateCentroid();
                Gl.glVertex2d(p.X, p.Y);
            }
            Gl.glEnd();
        }

        public void DrawTriangle(Color color, Point a, Point b, Point c)
        {
            Gl.glBegin(Gl.GL_LINE_STRIP);
            {
                Gl.glVertex2d(a.X, a.Y);
                Gl.glVertex2d(b.X, b.Y);
                Gl.glVertex2d(c.X, c.Y);
                Gl.glVertex2d(a.X, a.Y);
            }
            Gl.glEnd();
        }


        public void DrawTriangles(Color color)
        {
            GLUtil.SetColor(color);

            foreach (Triangle triangle in _indexedTriMesh)
            {
                DrawTriangle(color, triangle.A, triangle.B, triangle.C);
                DrawTriangleCentroid(color, triangle);
            }
        }

        public void Render()
        {
            if (_polygon == null)
            {
                return;
            }

            PrimitiveDrawer.DrawPolygon(_polygon, new Engine.Color(1, 0, 0, 1));
            PrimitiveDrawer.DrawFilledPolygon(_polygon, new Engine.Color(1, 0, 0, 0.25f));
            DrawTriangles(new Color(1,1, 0,1));
            //DrawNodePaths(new Color(0, 1, 0, 1));

        }



        public void DrawNodePaths(Color color)
        {
            GLUtil.SetColor(color);
            foreach (NavNode startNode in _allNodes)
            {
                foreach (NavNode endNode in startNode.Links)
                {
                    PrimitiveDrawer.DrawLine2d(startNode.Position, endNode.Position);
                }
            }
        }

    }
}
