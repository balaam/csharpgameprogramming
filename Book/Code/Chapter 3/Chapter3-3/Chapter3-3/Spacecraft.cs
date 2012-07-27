using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter3_3
{
    public class Spacecraft
    {
        int _health = 10;
        bool _dead = false;
        int _shieldPower = 2;
        void OnShot()
        {
            if (_shieldPower > 0)
            {
                _shieldPower--;
                return;
            }

            _health--;
            if (_health <= 0)
            {
                _dead = true;
            }
        }  

    }
}
