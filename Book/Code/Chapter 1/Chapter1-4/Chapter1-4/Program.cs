using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Using generics instead of ArrayLists
            List<float> _list = new List<float>();

            // no boxing
            _list.Add(1);           
            _list.Add(5.6f);

            // no unboxing
            float valueOne = _list[0]; 
            float valueTwo = _list[1];
        }
    }
}
