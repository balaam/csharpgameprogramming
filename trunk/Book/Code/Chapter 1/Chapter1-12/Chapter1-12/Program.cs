using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_12
{
    class Program
    {
        class User
        {
            string _name;

            // Lots of specific user code would be here in a full program

            public User(string name)
            {
                _name = name;
            }

            public string GetName()
            {
                return _name;
            }
        }

        class Monster
        {
            string _name;

            // Lots of specific monster code would be here in a full program

            public Monster(string name)
            {
                _name = name;
            }

            public string GetName()
            {
                return _name;
            }

        }

        void PrintName(dynamic obj)
        {
	        System.Console.WriteLine(obj.GetName());
        }

       

        static void Main(string[] args)
        {
            Program program = new Program();
            program.PrintName(new User("Bob")); // Bob
            program.PrintName(new Monster("Axeface")); // Axeface
        }
    }
}
