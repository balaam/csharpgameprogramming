using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace Engine.Input
{
    public class Input
    {
        public Mouse Mouse { get; set; }
        public Keyboard Keyboard { get; set; }
        bool _usingController = false;
        public XboxController Controller { get; set; }

        public Input()
        {
            Sdl.SDL_InitSubSystem(Sdl.SDL_INIT_JOYSTICK);
            if (Sdl.SDL_NumJoysticks() > 0)
            {
                Controller = new XboxController(0);
                _usingController = true;
            }
        }

        public void Update(double elapsedTime)
        {
            StandardUpdate();
            Mouse.Update(elapsedTime);
        }

        public void Update(double elapsedTime, double width, double height)
        {
            StandardUpdate();
            Mouse.Update(elapsedTime, Vector.Zero, width, height);
        }

        public void Update(double elapsedTime, Vector origin, double width, double height)
        {
            StandardUpdate();
            Mouse.Update(elapsedTime, origin, width, height);
        }

        private void StandardUpdate()
        {
            if (_usingController)
            {
                Sdl.SDL_JoystickUpdate();
                Controller.Update();
            }
            Keyboard.Process();
        }


  
    }
}
