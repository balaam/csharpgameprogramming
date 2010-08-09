using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.PathFinding
{
    public class PathFinder
    {
        AStar<NavigationNode> _astar;
        public PathFinder()
        {
        }

        public List<Point> GetPath(Point from, Point to, NavMesh navMesh)
        {
            List<Point> path = new List<Point>();
            // First find the polygon they're in 
            // !* May want a little overlapping so all screen can be clicked.
            //    In that case this should check if all intersected options contain as same poly start/end
            ConvexPolygon polyStart = navMesh.PolygonList.First(x => x.Intersects(from));
            ConvexPolygon polyEnd = navMesh.PolygonList.First(x => x.Intersects(to));

            if (polyStart == null || polyEnd == null)
            {
                return path;
            }
            else if (polyStart == polyEnd)
            {
                path.Add(from);
                path.Add(to);
            }
            else if (polyStart != polyEnd)
            {
                // This does not need doing every time but it's easier to code if is recreated.
                _astar = new AStar<NavigationNode>(

                 delegate(NavigationNode startNode, NavigationNode endNode)
                 {
                     return Math.Sqrt(startNode.Position.X * endNode.Position.X
                                    + startNode.Position.Y * endNode.Position.Y);
                 });


                var startEndNodes = CreateNodeNetwork(polyStart, polyEnd, from, to, navMesh);
                _astar.FindPath(startEndNodes.Item1, startEndNodes.Item2);
                _astar.Path.Reverse();
                foreach (var node in _astar.Path)
                {
                    path.Add(node.Position);
                }
            }


            return path;
        }

        private List<NavigationNode> _nodeGraph = new List<NavigationNode>();
        /// <summary>
        /// Works out a graph of nodes on top of the navmesh.
        /// Each polygon is a node and a node is added a connecting edges of linked polygons.
        /// The node graph class member is filled in by running this method.
        /// It's quite likely this code isn't very optimal.
        /// </summary>
        /// <param name="polyStart">The polygon the entity is currently in.</param>
        /// <param name="polyEnd">The polygon the entity would like to be in.</param>
        /// <param name="from">The point in the entity is current positioned.</param>
        /// <param name="to">The point the entity would like to be positioned.</param>
        /// <param name="navMesh">The navigation mesh describing the polygons and their links.</param>
        /// <returns>The start and end nodes for the journey the player wants to take</returns>
        private Tuple<NavigationNode, NavigationNode> CreateNodeNetwork(ConvexPolygon polyStart, ConvexPolygon polyEnd, Point from, Point to, NavMesh navMesh)
        {
            _nodeGraph.Clear();
            // Store a map poly -> node to make it simple to work out the connection nodes.
            Dictionary<ConvexPolygon, NavigationNode> polyToNodeMap = new Dictionary<ConvexPolygon, NavigationNode>();

            NavigationNode startNode = null;
            NavigationNode endNode = null;

            // Create a node for the centroid of each polygon
            // Replace the postion of the start and end polygon.
            foreach (ConvexPolygon polygon in navMesh.PolygonList)
            {
                Point position;
                NavigationNode node;
                if (polyStart == polygon)
                {
                    position = from;
                    node = new NavigationNode(position);
                    startNode = node;
                }
                else if (polyEnd == polygon)
                {
                    position = to;
                    node = new NavigationNode(position);
                    endNode = node;
                }
                else
                {
                    position = polygon.CalculateCentroid();
                    node = new NavigationNode(position);
                }


                _nodeGraph.Add(node);
                polyToNodeMap.Add(polygon, node);
            }

            // Create the edge nodes and add the links
            // !* This is where you'd add several nodes per edge, if you wanted.
            foreach (PolygonLink link in navMesh.Links)
            {
                LineSegment line = link.GetShortestEdge();
                Point midPoint = line.GetMiddle();

                NavigationNode connectionNode = new NavigationNode(midPoint);

                // Add bidirectional links to connected polys and edge polys.
                polyToNodeMap[link.StartPoly].Neighbours.Add(connectionNode);
                connectionNode.Neighbours.Add(polyToNodeMap[link.StartPoly]);

                polyToNodeMap[link.EndPoly].Neighbours.Add(connectionNode);
                connectionNode.Neighbours.Add(polyToNodeMap[link.EndPoly]);

                _nodeGraph.Add(connectionNode);
            }

            return new Tuple<NavigationNode, NavigationNode>(startNode, endNode);
        }

    }
}
