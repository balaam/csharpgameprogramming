using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.PathFinding
{
    /// <summary>
    /// If a node wants to be used by the AStar class it needs to implement this interface.
    /// <code>class classname : IAStarNode<classname></code>
    /// </summary>
    /// <typeparam name="T">This should be whatever class is implementing the interface.</typeparam>
    public interface IAStarNode<T>
    {
        /// <summary>
        /// The nodes that this node has links to.
        /// </summary>
        List<T> Neighbours { get; set; }

        /// <summary>
        /// The base cost of this node. 
        /// Running AStar will alter this value.
        /// </summary>
        double Cost { get; set; }

        /// <summary>
        /// The parent node information is used by AStar to help find a path.
        /// </summary>
        T ParentNode { get; set; }
    }
}
