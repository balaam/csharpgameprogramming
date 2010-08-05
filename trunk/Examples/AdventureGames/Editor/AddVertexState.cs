using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Engine.PathFinding;

namespace Editor
{
    class AddVertexState : IGameObject
    {
        Input _input;
        NavMesh _navMesh;
        ConvexPolygon _selectedPolygon = null;
        public AddVertexState(Input input, NavMesh navMesh)
        {
            _input = input;
            _navMesh = navMesh;
        }

        public void Activated()
        {
        }

        public void Update(double elapsedTime)
        {
            _selectedPolygon = _navMesh.FindNearestPolygon(_input.Mouse.Position);
            if (_input.Mouse.LeftPressed)
            {
                if (_selectedPolygon != null)
                {
                    _selectedPolygon.TryToAddVertex(_input.Mouse.Position);
                }
            }
        }

        private void RenderEditableVertices()
        {
            if (_selectedPolygon == null)
            {
                return;
            }

            for (int i = 0; i < _selectedPolygon.Vertices.Count; i++)
            {
     
               GLUtil.DrawCircle(_selectedPolygon.Vertices[i], 15, new Color(0, 0, 0, 1));

            }
        }

        public void Render()
        {
            RenderEditableVertices();
        }
    }
}
