using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_8
{
    class Program
    {
        class Monster
        {
            string _name;
            int _currentLevel = 1;
            int _curentExperience = 0;
            int _nextLevelExperience = 1000;

            public Monster(string name, int curentExperience)
            {
                _name = name;
                _curentExperience = curentExperience;
            }

            public string Name()
            {
                return _name;
            }
                
            public int CurrentExperience()
            {
                return _curentExperience;
            }

            public int NextLevelRequiredExperience()
            {
                return _nextLevelExperience;
            }

            public void LevelUp()
            {
                Console.WriteLine(_name + " has levelled up!");
                _currentLevel++;
                _curentExperience = 0;
                _nextLevelExperience = _currentLevel * 1000;
            }

        }

        static void Main(string[] args)
        {
            List<Monster> _monsterList = new List<Monster>();
            _monsterList.Add(new Monster("Ogre", 1001));
            _monsterList.Add(new Monster("Skeleton", 999));
            _monsterList.Add(new Monster("Giant Bat", 1004));
            _monsterList.Add(new Monster("Slime", 0));


            // Select monsters that are about to level up
            IEnumerable<Monster> query = from m in _monsterList
                                         where m.CurrentExperience() >
                                               m.NextLevelRequiredExperience()
                                         orderby m.Name() descending
                                         select m;

            // Level up all selected monsters.
            foreach (Monster m in query)
            {
                m.LevelUp();
            }
                
        }
    }
}
