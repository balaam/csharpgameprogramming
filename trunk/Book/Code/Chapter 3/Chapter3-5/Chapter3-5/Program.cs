using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter3_5
{
    class Program
    {
        static void Main(string[] args)
        {
            
            PlayerTests playerTests = new PlayerTests();
            
            // This test will now pass, the player is given 10 health in the constructor.
            Console.WriteLine("Test Player Is Alive When Born:");
            Console.WriteLine("Passed: " + playerTests.TestPlayerIsAliveWhenBorn().ToString());

            Console.WriteLine("Test Player Is Hurt When Hit:");
            Console.WriteLine("Passed: " + playerTests.TestPlayerIsHurtWhenHit());

            Console.WriteLine("Debugging an issue with determing if the player is dead:");
            Console.WriteLine("Passed: " + playerTests.TestPlayerShouldBeDead());

            Console.ReadKey();
        }
    }
}
