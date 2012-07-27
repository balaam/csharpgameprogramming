using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter4_3
{
    class GameCreature
    {
        int _health = 10;
        int _armor = 1;

        public void TakeDamage(int damage)
        {
            _health = _health - Math.Max(0, damage - _armor);
        }
    }
}
