using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_13
{
    class Program
    {
        public dynamic GetDynamicObject()
        {
            return new object();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            dynamic obj = program.GetDynamicObject();
            obj.ICanCallAnyFunctionILike();
        }
    }
}
