using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    /// <summary>
    /// </summary>
    public class Interpolation
    {
        /// <summary>
        /// Lerp a value from one range to another
        /// </summary>
        /// <example>
        /// Lerp(5, 0, 10, 0, 1) == 0.5
        /// Lerp(5, 0, 10, 0, 1)
        /// </example>
        /// <param name="value"></param>
        /// <param name="inStart"></param>
        /// <param name="inEnd"></param>
        /// <param name="outStart"></param>
        /// <param name="outEnd"></param>
        /// <returns></returns>
        public static double Lerp(double value, double inStart, double inEnd, double outStart, double outEnd)
        {
            double normed = (value - inStart) / (inEnd - inStart);
            return outStart + (normed * (outEnd - outStart));
        }
    }
}
