using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter7_2
{
    public interface IGameObject
    {
        void Update(double elapsedTime);
        void Render();
    }
}
