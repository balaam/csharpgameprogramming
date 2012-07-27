using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_3
{
    class Program
    {

        class Int
        {
            int _value;

            public Int(int value)
            {
                _value = value;
            }

            public void SetValue(int value)
            {
                _value = value;
            }

            public override string ToString()
            {
                return _value.ToString();
            }
        }

        static void Main(string[] args)
        {

            ValueTypeExample();
            ReferenceTypeExample();
        }


        private static void ValueTypeExample()
        {
            int i = 100;
            int j = i; // value type so copied into i
            Console.WriteLine(i); // 100
            Console.WriteLine(j); // 100

            i = 30;
            Console.WriteLine(i); // 30
            Console.WriteLine(j); // 100
        }

        private static void ReferenceTypeExample()
        {
            Int i = new Int(100);
            Int j = i; // reference type so reference copied into i
            Console.WriteLine(i); // 100
            Console.WriteLine(j); // 100

            i.SetValue(30);
            Console.WriteLine(i); // 30
            Console.WriteLine(j); // 30 <- the shocking difference
        }

    }
}
