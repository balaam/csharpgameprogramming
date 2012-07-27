using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter6_5
{
    class TitleMenuState : IGameObject
    {
        double _currentRotation = 0;
        #region IGameObject Members

        public void Update(double elapsedTime)
        {
           // Console.WriteLine("TitleMenuState process"); <- writing too much to the console will slow things down - a lot!
            _currentRotation = 10 * elapsedTime;
        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glPointSize(5.0f);

            Gl.glRotated(_currentRotation, 0, 1, 0);
            Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
            {
                Gl.glColor4d(1.0, 0.0, 0.0, 0.5);
                Gl.glVertex3d(-50, 0, 0);
                Gl.glColor3d(0.0, 1.0, 0.0);
                Gl.glVertex3d(50, 0, 0);
                Gl.glColor3d(0.0, 0.0, 1.0);
                Gl.glVertex3d(0, 50, 0);

            }
            Gl.glEnd();
            Gl.glFinish();
        }

        #endregion

    }
}
