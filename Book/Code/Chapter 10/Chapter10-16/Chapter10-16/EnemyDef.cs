using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter10_16
{
    public class EnemyDef
    {
        public string EnemyType { get; set; }
        public double LaunchTime { get; set; }

        public EnemyDef()
        {
            EnemyType = "cannon_fodder";
            LaunchTime = 0;
        }

        public EnemyDef(string enemyType, double launchTime)
        {
            EnemyType = enemyType;
            LaunchTime = launchTime;
        }
    }
}
