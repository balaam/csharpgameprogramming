using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Chapter1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Code with out the user of generics.
            ArrayList _list = new ArrayList();
            _list.Add(1.3); // boxing converts value type to a reference type
            _list.Add(1.0);

            //Unboxing Converts reference type to a value type.
            object objectAtPositionZero = _list[0];
            double valueOne = (double)objectAtPositionZero;
            double valueTwo = (double)_list[1];

        }
    }
}
