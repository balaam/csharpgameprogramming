using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter8_7
{
    class CircleIntersectionState : IGameObject
    {
        Circle _circle = new Circle(Vector.Zero, 200);
        Input _input;
        public CircleIntersectionState(Input input)
        {
            _input = input;
            Gl.glLineWidth(3);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }
        #region IGameObject Members

        public void Update(double elapsedTime)
        {
            if (_circle.Intersects(_input.MousePosition))
            {
                _circle.Color = new Color(1, 0, 0, 1);
            }
            else
            {
                // If the circle’s not intersected turn it back to white.
                _circle.Color = new Color(1, 1, 1, 1);
            }

        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _circle.Draw();

            // Draw the mouse cursor as a point
            Gl.glPointSize(5);
            Gl.glBegin(Gl.GL_POINTS);
            {
                Gl.glVertex2f(_input.MousePosition.X,
                    _input.MousePosition.Y);
            }
            Gl.glEnd();

        }
        #endregion

    }
}
