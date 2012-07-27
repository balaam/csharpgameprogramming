using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter8_6
{
    public class FramesPerSecond
    {
        int _numberOfFrames = 0;
        double _timePassed = 0;
        public double CurrentFPS { get; set; }

        public void Process(double timeElapssed)
        {
            _numberOfFrames++;
            _timePassed = _timePassed + timeElapssed;

            if (_timePassed > 1)
            {
                CurrentFPS = (double)_numberOfFrames / _timePassed;
                _timePassed = 0;
                _numberOfFrames = 0;
            }
        }
    }

}
