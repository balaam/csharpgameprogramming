using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter3_5
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

        public bool TestPlayerIsHurtWhenHit()
        {
            Player p = new Player();
            int oldHealth = p.Health;
            p.OnHit();
            if (p.Health < oldHealth)
            {
                return true;
            }
            return false;
        }

        public bool TestPlayerIsDeadWhenHasNoHealth()
        {
            Player p = new Player();
            p.Health = 0;
            if (p.IsDead())
            {
                return true;
            }
            return false;
        }

        	public bool TestPlayerShouldBeDead()
	{
        Player p = new Player();
		p.Health = 2; // give him low health
		
		// Now hit him a lot, as in debugging
		p.OnHit();
		p.OnHit();
		p.OnHit();
		p.OnHit();
		p.OnHit();
		
		if ( p.IsDead() )
		{
			return true;
		}
		return false;
		
	}


    }
}
