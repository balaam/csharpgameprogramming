using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WalkablePolygonCodeSketch
{
    class AStar<T>
    {
        List<IPathNode<T>> _openList = new List<IPathNode<T>>();
        List<IPathNode<T>> _closeList = new List<IPathNode<T>>();
        IPathNode<T> _goal;
        List<T> _path = new List<T>();

        public List<T> Path
        {
            get
            {
                return _path;
            }
        }


        public void FindPath(IPathNode<T> root, IPathNode<T> goal)
        {
            _goal = goal;
            _openList.Clear();
            _closeList.Clear();

            root.ParentNode = null;
            _openList.Add(root);

            ProcessPath();
        }

        private void ProcessPath()
        {
            bool goalHasBeenReached = false;
            while (goalHasBeenReached == false)
            {
                _openList.OrderByDescending(x => x.Cost);
                IPathNode<T> current = _openList.FirstOrDefault();
               

                if (current == _goal)
                {
                    
                    // Backtrace and return path.
                    IPathNode<T> trace = _goal;
                    while(trace != null)
                    {
                        _path.Add((T)trace);
                        trace = trace.ParentNode;
                    }
                    return;
                }

                _openList.RemoveAt(0);
                _closeList.Add(current);

                foreach (IPathNode<T> neighbour in current.Neighbours)
                {
                    System.Diagnostics.Debug.Assert(neighbour != current, "No node should be it's own neighbour.");
              
                    double cost = current.Cost + neighbour.CalculateTravelCost((T)_goal);

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
