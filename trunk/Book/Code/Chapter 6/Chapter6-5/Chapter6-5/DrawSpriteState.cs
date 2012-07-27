using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter6_5
{
    class DrawSpriteState : IGameObject
    {
        public void Update(double elapsedTime)
        {

        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glBegin(Gl.GL_TRIANGLES);
            {
                Gl.glVertex3d(-100, 100, 0); // top left
                Gl.glVertex3d(100, 100, 0); // top right
                Gl.glVertex3d(-100, -100, 0); // bottom left

                // Uncomment these three lines to draw the other half of the quad.
                //Gl.glVertex3d(100, 100, 0); // top right
                //Gl.glVertex3d(100, -100, 0); // bottom right
                //Gl.glVertex3d(-100, -100, 0); // bottom left

            }
            Gl.glEnd();



            //
            // Comment out the above code (from glBegin to glEnd)
            // and use this to easily edit the width and height of the quad.
            //
            //double height = 200;
            //double width = 200;
            //double halfHeight = height / 2;
            //double halfWidth = width / 2;


            //Gl.glBegin(Gl.GL_TRIANGLES);
            //{
            //    Gl.glVertex3d(-halfWidth, halfHeight, 0); // top left
            //    Gl.glVertex3d(halfWidth, halfHeight, 0); // top right
            //    Gl.glVertex3d(-halfWidth, -halfHeight, 0); // bottom left

            //    Gl.glVertex3d(halfWidth, halfHeight, 0); // top right
            //    Gl.glVertex3d(halfWidth, -halfHeight, 0); // bottom right
            //    Gl.glVertex3d(-halfWidth, -halfHeight, 0); // bottom left
            //}
            //Gl.glEnd();

        }
    }
}
