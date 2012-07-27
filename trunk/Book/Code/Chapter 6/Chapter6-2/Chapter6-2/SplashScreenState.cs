using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter6_2
{
    class SplashScreenState : IGameObject
    {
        StateSystem _system;
        public SplashScreenState(StateSystem system)
        {
            _system = system;
        }

        #region IGameObject Members

        public void Update(double elapsedTime)
        {
            // Wait so many seconds then call _system.ChangeState("title_menu")
            System.Console.WriteLine("Processing Splash");
        }

        public void Render()
        {
            System.Console.WriteLine("Rendering Splash");
        }

        #endregion

    }
}
