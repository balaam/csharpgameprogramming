using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public interface IGameObject
    {
        void Update(double elapsedTime);
        void Render();
    }
}
