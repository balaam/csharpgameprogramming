using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch.ConvexPolyDrawer
{
    class PathFinder
    {
        class PathNode : IPathNode<PathNode>
        {
            IPathNode<PathNode> _pathNode;
            List<PathNode> _neighbours = new List<PathNode>();
            double _cost;
            public Point Position { get; set; }

            public PathNode(Point position)
            {

                Position = position;
            }
           
            public IPathNode<PathNode> ParentNode
            {
                get
                {
                    return _pathNode;
                }
                set
                {
                    _pathNode = value;
                }
            }

            public List<PathNode> Neighbours
            {
                get { return _neighbours;  }
            }

            public double Cost
            {
                get
                {
                    return _cost;
                }
                set
                {
                    _cost = value;
                }
            }

            public double CalculateTravelCost(PathNode node)
            {
                return Math.Sqrt(node.Position.X * Position.X + node.Position.Y * Position.Y);
            }
        }

        private List<PathNode> _nodeGraph = new List<PathNode>();

        private List<ConvexPolyForm> _polygons;
        private List<Connection> _connections;

        public List<ConvexPolyForm> Polygons
        {
            get
            {
                return _polygons;
            }
        }

        public List<Connection> Connections
        {
            get
            {
                return _connections;
            }
        }

        public void Init(List<ConvexPolyForm> polygons, List<Connection> connections)
        {
            _polygons = polygons;
            _connections = connections;
        }

        public List<Point> GetPath(Point from, Point to)
        {
            List<Point> path = new List<Point>();
            // First find the polygon they're in 
            // !* May want a little overlapping so all screen can be clicked.
            //    In that case this should check if all intersected options contain as same poly start/end
            ConvexPolyForm polyStart    = FindPoly(from);
            ConvexPolyForm polyEnd = FindPoly(to);

            if (polyStart == null || polyEnd == null)
            {
                return path;
            }
            else if (polyStart == polyEnd)
            {
                path.Add(from);
                path.Add(to);
            }
            else if(polyStart != polyEnd)
            {
                // This does not need doing every time but it's easier to code if is recreated.
                var startEndNodes = CreateNodeNetwork(polyStart, polyEnd, from, to);
                AStar<PathNode> astar = new AStar<PathNode>();
                astar.FindPath(startEndNodes.Item1, startEndNodes.Item2);
                astar.Path.Reverse();
                foreach (var node in astar.Path)
                {
                    path.Add(node.Position);
                }
            }
            

            return path;
        }

        private Tuple<PathNode, PathNode> CreateNodeNetwork(ConvexPolyForm polyStart, ConvexPolyForm polyEnd, Point from, Point to)
        {
            _nodeGraph.Clear();
            // Store a map poly -> node to make it simple to work out the connection nodes.
            Dictionary<ConvexPolyForm, PathNode> polyToNodeMap = new Dictionary<ConvexPolyForm, PathNode>();

            PathNode startNode = null;
            PathNode endNode = null;

            // Create a node for the centroid of each polygon
            // Replace the postion of the start and end polygon.
            foreach (ConvexPolyForm poly in _polygons)
            {
                Point position;
                PathNode node;
                if (polyStart == poly)
                {
                    position = from;
                    node = new PathNode(position);
                    startNode = node;
                }
                else if (polyEnd == poly)
                {
                    position = to;
                    node = new PathNode(position);
                    endNode = node;
                }
                else
                {
                    position = poly.GetCentroid();
                    node = new PathNode(position);
                }

                 
                _nodeGraph.Add(node);
                polyToNodeMap.Add(poly, node);
            }

            // Create the edge nodes and add the links
            // !* This is where you'd add several nodes per edge, if you wanted.
            foreach (Connection connection in _connections)
            {
                LineSegment line = connection.GetShortestEdge();
                Point midPoint = line.GetMiddle();

                PathNode connectionNode = new PathNode(midPoint);

                // Add bidirectional links to connected polys and edge polys.
                polyToNodeMap[connection.StartPoly].Neighbours.Add(connectionNode);
                connectionNode.Neighbours.Add(polyToNodeMap[connection.StartPoly]);

                polyToNodeMap[connection.EndPoly].Neighbours.Add(connectionNode);
                connectionNode.Neighbours.Add(polyToNodeMap[connection.EndPoly]);

                _nodeGraph.Add(connectionNode);
            }

            return new Tuple<PathNode, PathNode>(startNode, endNode);
        }

        private ConvexPolyForm FindPoly(Point from)
        {
            foreach (ConvexPolyForm polygon in _polygons)
            {
                if(polygon.Intersects(from.X, from.Y))
                {
                    return polygon;
                }
            }
            return null;
        }
    }
}
