using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter4_2
{
    class GameThingUser
    {
        GameThing _gameThing;

        public GameThingUser()
        {
            GameThing gt = new GameThing();
            _gameThing = gt;
        }


        public GameThing GetGameThing(string id)
        {
            return new GameThing();
        }

        public GameThing CloneGameThing(GameThing gt)
        {
            return new GameThing();
        }
    }
}
