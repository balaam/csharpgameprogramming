using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter3_1
{
    public class FlyingSaucer
    {
        // ... additional code may go here
        int _health = 10;
        bool _dead = false;
        void OnShot()
        {
            _health--;
            if (_health <= 0)
            {
                _dead = true;
            }
        }
        // additional code may go here...
    }
}
