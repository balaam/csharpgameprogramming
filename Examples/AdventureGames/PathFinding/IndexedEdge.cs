using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.PathFinding
{
    /// <summary>
    /// This is an edge represented by two indices to the edges positions (in a vertex array for example)
    /// </summary>
    public struct IndexedEdge
    {
        public int Start;
        public int End;
        public IndexedEdge(int start, int end)
            : this()
        {
            Start = start;
            End = end;
        }
    }
    
}
