using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_7
{
    class Program
    {
        int SumArrayOfValues(int[] array)
        {
            int sum = 0;
            Array.ForEach(
                array,
                delegate(int value)
                {
                    sum += value;
                }
            );
            return sum;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            int i = p.SumArrayOfValues(new int []{1, 2, 3});
            Console.WriteLine(i);
        }
    }
}
