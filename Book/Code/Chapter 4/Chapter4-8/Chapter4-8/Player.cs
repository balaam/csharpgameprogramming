using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerTest
{
    class Player
    {
        bool _enlarged = false;
        internal bool IsEnlarged()
        {
            return _enlarged;
        }

        internal void Eat(string thingToEat)
        {
            if (thingToEat == "mushroom")
            {
                _enlarged = true;
            }
        }

    }
}
