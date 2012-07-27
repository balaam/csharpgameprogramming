using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter8_6
{
    public interface IGameObject
    {
        void Update(double elapsedTime);
        void Render();
    }
}
