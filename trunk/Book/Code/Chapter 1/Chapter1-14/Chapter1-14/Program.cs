using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_14
{
    class Program
    {
        class Player
        {
            // Knock the player backwards
            public void KnockBack(int knockBackDamage = 0)
            {
                // code
            }
        }



        static void Main(string[] args)
        {
            Player p = new Player();
            p.KnockBack(); // knocks the player back and by default deals no damage
            p.KnockBack(10); // knocks the player back and gives 10 damage
        }
    }
}
