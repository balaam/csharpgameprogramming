using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WalkablePolygonCodeSketch
{
    interface IPathNode<T>
    {
        IPathNode<T> ParentNode { get; set; }
        List<T> Neighbours { get; }
        double Cost { get; set; }
        double CalculateTravelCost(T node);

        
    }
}
