using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace Chapter8_10
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Point(float x, float y)
            : this()
        {
            X = x;
            Y = y;
        }
    }

}
