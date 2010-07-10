using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace Engine.Input
{
    public class ControllerButton
    {
        IntPtr _joystick;
        int _buttonId;
        bool _wasHeld = false;
        public bool Pressed { get; private set; }

        public bool Held { get; private set; }

        public ControllerButton(IntPtr joystick, int buttonId)
        {
            _joystick = joystick;
            _buttonId = buttonId;
        }


        public void Update()
        {
            // reset the pressed value
            Pressed = false;

            byte buttonState = Sdl.SDL_JoystickGetButton(_joystick, _buttonId);
            Held = (buttonState == 1);

            if (Held)
            {
                if (_wasHeld == false)
                {
                    Pressed = true;
                }
                _wasHeld = true;
            }
            else
            {
                _wasHeld = false;
            }
        }


    }
}
