using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch
{
    class NavNode : IPathNode<NavNode>
    {
        Point _position = new Point();
        List<NavNode> _links = new List<NavNode>();
        List<Tuple<int, int>> _nodeEdges = new List<Tuple<int, int>>();

        // Which edge adjacent nodes are seperated by. No single vertex seperation.
        Dictionary<NavNode, Tuple<int, int>> _nodeToEdge = new Dictionary<NavNode, Tuple<int, int>>();

        public Point Position
        {
            get
            {
                return _position;
            }
        }

        public List<NavNode> Links
        {
            get
            {
                return _links;
            }
        }

        public NavNode(Point position)
        {
            _position = position;
        }

        internal bool ConnectsTo(NavNode foundNode)
        {
            return _links.Contains(foundNode);
        }

     
        public IPathNode<NavNode> ParentNode { get; set; }


        public List<NavNode> Neighbours
        {
            get { return _links; }
        }

        public double Cost { get; set; }
   

        public double CalculateTravelCost(NavNode node)
        {
            return Math.Sqrt(node.Position.X * Position.X + node.Position.Y * Position.Y);
        }

        internal void AddEdge(Tuple<int, int> edge)
        {
            _nodeEdges.Add(edge);
        }

        bool IsEdgeEqual(Tuple<int, int> edge1, Tuple<int, int> edge2)
        {
            if (edge1.Item1 == edge2.Item1 && edge1.Item2 == edge2.Item2)
            {
                return true;
            }
            else if(edge1.Item1 == edge2.Item2 && edge1.Item2 == edge2.Item1)
            {
                return true;
            }
            return false;
        }

        internal Tuple<int, int> FindSharedEdge(NavNode otherNode)
        {
            foreach (var myEdge in _nodeEdges)
            {
                foreach (var yourEdge in otherNode._nodeEdges)
                {
                    if (IsEdgeEqual(myEdge, yourEdge))
                    {
                        return myEdge;
                    }
                }
            }
            return null;
        }

        internal void AddAdjacentNode(NavNode otherNode, Tuple<int, int> edge)
        {
            _links.Add(otherNode);
            _nodeToEdge.Add(otherNode, edge);

        }

        internal Tuple<int,int> GetNeighboursEdge(NavNode node)
        {
            return _nodeToEdge[node];
        }
    }
}
