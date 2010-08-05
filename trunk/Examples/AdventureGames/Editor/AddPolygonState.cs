using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Engine.PathFinding;

namespace Editor
{
    class AddPolygonState : IGameObject
    {
        Input _input;
        NavMesh _navMesh;
        public AddPolygonState(Input input, NavMesh navMesh)
        {
            _input = input;
            _navMesh = navMesh;
        }

        public void Activated()
        {
        }

        public void Update(double elapsedTime)
        {
            if (_input.Mouse.LeftPressed)
            {
                _navMesh.AddPolygon(new ConvexPolygon(_input.Mouse.Position));
            }
        }

        public void Render()
        {
        }
    }
}
