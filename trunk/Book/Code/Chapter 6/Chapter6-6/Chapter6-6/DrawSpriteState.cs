using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter6_6
{
    class DrawSpriteState : IGameObject
    {

        TextureManager _textureManager;

        public DrawSpriteState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            Texture texture = _textureManager.Get("face");
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.Id);

        }


        public void Update(double elapsedTime)
        {

        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            double height = 200;
            double width = 200;
            double halfHeight = height / 2;
            double halfWidth = width / 2;

            double x = 0;
            double y = 0;
            double z = 0;

            Gl.glBegin(Gl.GL_TRIANGLES);
            {
                Gl.glTexCoord2d(0, 0);
                Gl.glVertex3d(x - halfWidth, y + halfHeight, z); // top left
                Gl.glTexCoord2d(1, 0);
                Gl.glVertex3d(x + halfWidth, y + halfHeight, z); // top right
                Gl.glTexCoord2d(0, 1);
                Gl.glVertex3d(x - halfWidth, y - halfHeight, z); // bottom left

                // Uncomment these lines to see the full texture mapped quad.
                //Gl.glTexCoord2d(1, 0);
                //Gl.glVertex3d(x + halfWidth, y + halfHeight, z); // top right
                //Gl.glTexCoord2d(1, 1);
                //Gl.glVertex3d(x + halfWidth, y - halfHeight, z); // bottom right
                //Gl.glTexCoord2d(0, 1);
                //Gl.glVertex3d(x - halfWidth, y - halfHeight, z); // bottom left

            }
            Gl.glEnd();


        }
    }

}