using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter3_5
{
    class Player
    {
        public int Health { get; set; }

        public void OnHit()
        {
            // Fill in the code to pass the test.

        }


        public bool IsDead()
        {
            // Is this correct?
            return Health == 0;;
        }

        public Player()
        {
            Health = 10;
        }
    }
}
