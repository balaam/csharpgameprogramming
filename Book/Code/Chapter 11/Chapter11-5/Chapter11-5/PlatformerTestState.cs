using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using System.Drawing;
using Engine.Input;
using System.Windows.Forms;
using Tao.OpenGl;

namespace Chapter11_5
{
    class PlatfomerTestState : IGameObject
    {
        class PlatformEntity
        {
            const float _width = 16;
            const float _height = 16;
            RectangleF bounds = new RectangleF(-_width, -_height, _width, _height);

            public void Render()
            {
                Gl.glBegin(Gl.GL_LINE_LOOP);
                {
                    Gl.glColor3f(1, 0, 0);
                    Gl.glVertex2f(bounds.Left, bounds.Top);
                    Gl.glVertex2f(bounds.Right, bounds.Top);
                    Gl.glVertex2f(bounds.Right, bounds.Bottom);
                    Gl.glVertex2f(bounds.Left, bounds.Bottom);

                }
                Gl.glEnd();
                Gl.glEnable(Gl.GL_TEXTURE_2D);
            }

            public Vector GetPosition()
            {
                return new Vector(bounds.Location.X + _width, bounds.Location.Y + 16, 0);
            }

            public void SetPosition(Vector value)
            {
                bounds = new RectangleF((float)value.X - _width, (float)value.Y - _height, _width, _height);
            }
        }

        PlatformEntity _pc = new PlatformEntity();
        Input _input;

        double _speed = 1600;
        Vector _velocity = new Vector(0, 0, 0);
        bool _jumping = false;
        double _gravity = 0.75;
        double _friction = 0.1;

        public PlatfomerTestState(Input input)
        {
            _input = input;
        }

        #region IGameObject Members

        public void Update(double elapsedTime)
        {
            if (_input.Keyboard.IsKeyHeld(Keys.Left))
            {
                _velocity.X -= _speed;
            }
            else if (_input.Keyboard.IsKeyHeld(Keys.Right))
            {
                _velocity.X += _speed;
            }

            if (_input.Keyboard.IsKeyPressed(Keys.Up) && !_jumping)
            {
                _velocity.Y += 500;
                _jumping = true;
            }

            _velocity.Y -= _gravity;
            _velocity.X = _velocity.X * _friction;

            Vector newPosition = _pc.GetPosition();
            newPosition += _velocity * elapsedTime;


            if (newPosition.Y < 0)
            {
                newPosition.Y = 0;
                _velocity.Y = 0;
                _jumping = false;
            }
            _pc.SetPosition(newPosition);
        }

        public void Render()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glLineWidth(2.0f);
            Gl.glPointSize(10.0f);
            Gl.glColor3d(0, 0, 0);
            _pc.Render();
        }
        #endregion
    }

}
