using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter4_3
{
    class GameThingUser
    {
        GameCreature _gameThing;

        public GameThingUser()
        {
            GameCreature gt = new GameCreature();
            _gameThing = gt;
        }


        public GameCreature GetGameThing(string id)
        {
            return new GameCreature();
        }

        public GameCreature CloneGameThing(GameCreature gt)
        {
            return new GameCreature();
        }
    }
}
