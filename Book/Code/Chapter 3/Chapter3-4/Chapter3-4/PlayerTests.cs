using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter3_4
{
    class PlayerTests
    {
        public bool TestPlayerIsAliveWhenBorn()
        {
            Player p = new Player();
            if (p.Health > 0)
            {
                return true; // pass the test
            }
            return false; // fail the text
        }
    }

}
