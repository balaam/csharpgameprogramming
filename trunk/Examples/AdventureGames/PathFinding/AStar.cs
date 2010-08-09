using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.PathFinding
{
    /// <summary>
    /// AStar require it's nodes to implement certain functions (as defined by IAStarNode)
    /// If would be nice if this data could just be added to the node class then removed after AStar
    /// has done it's thing but I don't know a nice way of doing this.
    /// </summary>
    /// <typeparam name="T">The nodes AStar will operate on.</typeparam>
    public class AStar<T> where T : IAStarNode<T>, new()
    {
        public delegate double CostHeuristic(T start, T end);

        CostHeuristic   _costHeuristic;
        List<T>      _openList   = new List<T>();
        List<T>      _closeList  = new List<T>();
        T            _goal;
        List<T>      _path       = new List<T>();

        public List<T> Path
        {
            get{ return _path; }
        }


        public AStar(CostHeuristic costHeuristic)
        {
            _costHeuristic = costHeuristic;
        }

        public void FindPath(T root, T goal)
        {
            _goal = goal;
            _openList.Clear();
            _closeList.Clear();

            T rootNode = root;
            _openList.Add(rootNode);

            ProcessPath();
        }

            private void ProcessPath()
            {
                bool goalHasBeenReached = false;
                while (goalHasBeenReached == false)
                {
                    _openList.OrderByDescending(x => x.Cost);
                    T current = _openList.FirstOrDefault();


                    if (current.Equals(_goal))
                    {

                        // Backtrace and return path.
                        T trace = _goal;
                        while (trace != null)
                        {
                            _path.Add(trace);
                            trace = trace.ParentNode;
                        }
                        return;
                    }

                    _openList.RemoveAt(0);
                    _closeList.Add(current);

                    foreach (T neighbour in current.Neighbours)
                    {
                        System.Diagnostics.Debug.Assert(neighbour.Equals(current) == false, "No node should be it's own neighbour.");

                        double cost = current.Cost + _costHeuristic(neighbour, _goal);

                        if (_openList.Contains(neighbour) && neighbour.Cost > cost)
                        {
                            _openList.Remove(neighbour);
                        }

                        if (_closeList.Contains(neighbour) && neighbour.Cost > cost)
                        {
                            _closeList.Remove(neighbour);
                        }

                        if (_closeList.Contains(neighbour) == false &&
                            _openList.Contains(neighbour) == false)
                        {
                            neighbour.Cost = cost;
                            neighbour.ParentNode = current;
                            _openList.Add(neighbour);
                        }
                    }
                }
            }

        }
    
}
