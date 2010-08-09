using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.PathFinding
{
    /// <summary>
    /// A node used to create a graph over the navmesh.
    /// </summary>
    class NavigationNode : IAStarNode<NavigationNode>
    {
        public List<NavigationNode> Neighbours { get; set; }
        public Point Position { get; set; }
        public double Cost { get; set; }
        public NavigationNode ParentNode { get; set; }

        public NavigationNode()
        {
            Neighbours = new List<NavigationNode>();
        }

        public NavigationNode(Point position) : this()
        {
            Position = position;
        }
    }
}
