using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Chapter3_4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Here the test fails. The player has no health when born!
            PlayerTests playerTests = new PlayerTests();
            Console.WriteLine("Test Player Is Alive When Born:");
            Console.WriteLine("Passed: " + playerTests.TestPlayerIsAliveWhenBorn().ToString());

            Console.ReadKey();
        }
    }
}
