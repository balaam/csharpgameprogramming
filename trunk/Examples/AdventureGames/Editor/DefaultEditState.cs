using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Engine.PathFinding;

namespace Editor
{
    class DefaultEditState : IGameObject
    {
        Input _input;
        NavMesh _navMesh;

        const int VertexEditRadius = 15;
        ConvexPolygon _selectedPolygon;
        int _selectedVertexIndex = -1;

        bool _draggingVertex = false;

        public DefaultEditState(Input input, NavMesh navMesh)
        {
            _input = input;
            _navMesh = navMesh;
        }

        public void Activated()
        {
            _draggingVertex = false;
        }

        public void Update(double elapsedTime)
        {
            _selectedPolygon = _navMesh.FindNearestPolygon(_input.Mouse.Position);
            _selectedVertexIndex = FindSelectedVertex(_input.Mouse.Position, _selectedPolygon, VertexEditRadius);

            if (_draggingVertex)
            {
                if (_input.Mouse.LeftHeld && _selectedVertexIndex != -1)
                {
                    _selectedPolygon.TryToUpdateVertPosition(_selectedVertexIndex, _input.Mouse.Position);
                    return;
                }
                else
                {
                    _draggingVertex = false;
                }
            }

            if (_input.Keyboard.IsKeyPressed(System.Windows.Forms.Keys.A))
            {
                _navMesh.AddPolygon(new ConvexPolygon(_input.Mouse.Position));
            }

            if (_input.Mouse.LeftHeld && _selectedVertexIndex != -1)
            {
                _draggingVertex = true; 
            }

        }
        /// <summary>
        /// Checks all the vertices of a polygon and against a point.
        /// If that point is within the radius of a vertex, then the index to that vertex is returned.
        /// Otherwise -1 is returned.
        /// </summary>
        /// <param name="radius">The radius the position must come within for a vertex to be selected.</param>
        /// <param name="position">The poisition to check against the vertices</param>
        /// <param name="polygon">The polygon which will have it's vertices checked. A null polygon will return -1</param>
        /// <returns>Index to vertex in the radius of the position or minus one if no suitable vertex can be found.</returns>
        private int FindSelectedVertex(Point position, ConvexPolygon polygon, int radius)
        {
            if (polygon == null)
            {
                return -1;
            }

            for (int i = 0; i < polygon.Vertices.Count; i++)
            {
                Circle circle = new Circle(polygon.Vertices[i].X, polygon.Vertices[i].Y, radius);
                if (circle.Intersects(position))
                {
                    return i;
                }
            }
            return -1;
        }

        private void RenderEditableVertices()
        {
            if (_selectedPolygon == null)
            {
                return;
            }

            for (int i = 0; i < _selectedPolygon.Vertices.Count; i++)
            {
                if (i == _selectedVertexIndex)
                {
                    GLUtil.DrawCircle(_selectedPolygon.Vertices[i], 15, new Color(1, 1, 0, 1));
                }
                else
                {
                    GLUtil.DrawCircle(_selectedPolygon.Vertices[i], 15, new Color(0, 0, 0, 1));
                }
            }
        }

        public void Render()
        {
            RenderEditableVertices();
        }
    }
}
