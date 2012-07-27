using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter10_12
{
    class EnemyDef
    {
        public string EnemyType { get; set; }
        public Vector StartPosition { get; set; }
        public double LaunchTime { get; set; }

        public EnemyDef()
        {
            EnemyType = "cannon_fodder";
            StartPosition = new Vector(300, 0, 0);
            LaunchTime = 0;
        }

        public EnemyDef(string enemyType, Vector startPosition, double launchTime)
        {
            EnemyType = enemyType;
            StartPosition = startPosition;
            LaunchTime = launchTime;
        }


    }
}
