using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using System.Drawing;
using Tao.OpenGl;

namespace Chapter10_7
{
    class Enemy
    {
        Sprite _spaceship = new Sprite();
        double _scale = 0.3;
        public Enemy(TextureManager textureManager)
        {
            _spaceship.Texture = textureManager.Get("enemy_ship");
            _spaceship.SetScale(_scale, _scale);
            _spaceship.SetRotation(Math.PI); // make it face the player
            _spaceship.SetPosition(200, 0); // put it somewhere easy to see
        }

        public void Update(double elapsedTime)
        {
        }

        public void Render(Renderer renderer)
        {
            Render_Debug();
            renderer.DrawSprite(_spaceship);
        }

        public RectangleF GetBoundingBox()
        {
            float width = (float)(_spaceship.Texture.Width * _scale);
            float height = (float)(_spaceship.Texture.Height * _scale);
            return new RectangleF((float)_spaceship.GetPosition().X - width / 2,
                                    (float)_spaceship.GetPosition().Y - height / 2,
                                    width, height);

        }

        // Render a bounding box
        public void Render_Debug()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            RectangleF bounds = GetBoundingBox();
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


    }
}
