using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Tao.OpenGl;
using Tao.Sdl;
using Engine.Input;

namespace Chapter9_4
{
    class InputTestState : IGameObject
    {
        bool _useJoystick = false;
        XboxController _controller;

        public InputTestState()
        {
            Sdl.SDL_InitSubSystem(Sdl.SDL_INIT_JOYSTICK);
            if (Sdl.SDL_NumJoysticks() > 0)
            {
                // Start using the joystick code
                _useJoystick = true;
                _controller = new XboxController(0);
            }

        }

        private void DrawButtonPoint(bool held, int yPos)
        {
            if (held)
            {
                Gl.glColor3f(0, 1, 0);
            }
            else
            {
                Gl.glColor3f(0, 0, 0);
            }
            Gl.glVertex2f(-400, yPos);
        }


        public void Update(double elapsedTime)
        {
            if (_useJoystick == false)
            {
                return;
            }

            Sdl.SDL_JoystickUpdate();
            _controller.Update();
        }

        public void Render()
        {
            if (_useJoystick == false)
            {
                return;
            }
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

    

            Gl.glPointSize(10.0f);
            Gl.glBegin(Gl.GL_POINTS);
            {
                Gl.glColor3f(1, 0, 0);
                Gl.glVertex2f(
                    _controller.LeftControlStick.X * 300,
                    _controller.LeftControlStick.Y * -300);
                Gl.glColor3f(0, 1, 0);
                Gl.glVertex2f(
                    _controller.RightControlStick.X * 300,
                    _controller.RightControlStick.Y * -300);

                DrawButtonPoint(_controller.ButtonA.Held, 300);
                DrawButtonPoint(_controller.ButtonB.Held, 280);
                DrawButtonPoint(_controller.ButtonX.Held, 260);
                DrawButtonPoint(_controller.ButtonY.Held, 240);
                DrawButtonPoint(_controller.ButtonLB.Held, 220);
                DrawButtonPoint(_controller.ButtonRB.Held, 200);
                DrawButtonPoint(_controller.ButtonBack.Held, 180);
                DrawButtonPoint(_controller.ButtonStart.Held, 160);
                DrawButtonPoint(_controller.ButtonL3.Held, 140);
                DrawButtonPoint(_controller.ButtonR3.Held, 120);

                Gl.glColor3f(0.5f, 0, 0);
                Gl.glVertex2f(50, _controller.LeftTrigger.Value * 300);
                Gl.glColor3f(0, 0.5f, 0);
                Gl.glVertex2f(-50, _controller.RightTrigger.Value * 300);

            }
            Gl.glEnd();


        }

    }
}
