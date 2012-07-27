using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chapter1_10
{
    class Program
    {
        class Spaceship
        {
            public int Health { get; set; }
            public Spaceship()
            {
            }

        }

        static void NoObjectInitailizer()
        {
            Spaceship spaceship = new Spaceship();
            spaceship.Health = 30;
        }

        static void ObjectInitalizer()
        {
            Spaceship spaceship = new Spaceship()
            {
                Health = 30
            };
        }

        static void Main(string[] args)
        {
            

         
            

        }
    }
}
