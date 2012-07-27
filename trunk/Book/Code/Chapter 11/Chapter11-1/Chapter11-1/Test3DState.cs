using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Engine;
namespace Chapter11_1
{
    public class Test3DState : IGameObject
    {
        public Test3DState() { }
        public void Update(double elapsedTime) { }

        public void Render()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            // This is a simple way of using a camera
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Vector cameraPosition = new Vector(-75, 125, -500); // half a meter back on the Z axis
            Vector cameraLookAt = new Vector(0, 0, 0); // make the camera look at the world origin.
            Vector cameraUpVector = new Vector(0, 1, 0);
            Glu.gluLookAt(cameraPosition.X, cameraPosition.Y, cameraPosition.Z,
                            cameraLookAt.X, cameraLookAt.Y, cameraLookAt.Z,
                            cameraUpVector.X, cameraUpVector.Y, cameraUpVector.Z);


            // This draws a pyramid - can you make it draw a cube?
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            {
                Gl.glColor3d(1, 0, 0);
                Gl.glVertex3d(0.0f, 100, 0.0f);

                Gl.glColor3d(0, 1, 0);
                Gl.glVertex3d(-100, -100, 100);

                Gl.glColor3d(0, 0, 1);
                Gl.glVertex3d(100, -100, 100);

                Gl.glColor3d(0, 1, 0);
                Gl.glVertex3d(100, -100, -100);

                Gl.glColor3d(0, 0, 1);
                Gl.glVertex3d(-100, -100, -100);

                Gl.glColor3d(0, 1, 0);
                Gl.glVertex3d(-100, -100, 100);
            }
            Gl.glEnd();
        }
    }
}
