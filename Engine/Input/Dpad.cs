using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace Engine.Input
{
    public class DPad
    {
        IntPtr _joystick;
        int _index;

        public bool LeftHeld { get; private set; }
        public bool RightHeld { get; private set; }
        public bool UpHeld { get; private set; }
        public bool DownHeld { get; private set; }

        public DPad(IntPtr joystick, int index)
        {
            _joystick = joystick;
            _index = index;
        }

        public void Update()
        {
            byte b = Sdl.SDL_JoystickGetHat(_joystick, _index);
            UpHeld = (b == Sdl.SDL_HAT_UP);
            DownHeld = (b == Sdl.SDL_HAT_DOWN);
            LeftHeld = (b == Sdl.SDL_HAT_LEFT);
            RightHeld = (b == Sdl.SDL_HAT_RIGHT);
        }
    }
}
