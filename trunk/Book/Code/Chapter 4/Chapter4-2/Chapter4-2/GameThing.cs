using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter4_2
{

    // Right-click the GameThing class name and choose Refactor > Rename
    // Renamed it GameCreature, this will update all the references in GameThingUser
    class GameThing
    {
        int _health = 10;
        // Right click _var2 and choose Refactor > Rename, call it _armor instead.
        int _var2 = 1;

        // Right click HealthUpdate and rename it TakeDamage, rename v to damage
        public void HealthUpdate(int v)
        {
            _health = _health - Math.Max(0, v - _var2);
        }

    }
}
