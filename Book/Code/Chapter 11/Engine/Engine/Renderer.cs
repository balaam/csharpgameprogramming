using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Engine
{
    public class Renderer
    {
        public Renderer()
        {
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
        }

        public void DrawImmediateModeVertex(Vector position, Color color, Point uvs)
        {
            Gl.glColor4f(color.Red, color.Green, color.Blue, color.Alpha);
            Gl.glTexCoord2f(uvs.X, uvs.Y);
            Gl.glVertex3d(position.X, position.Y, position.Z);
        }

        Batch _batch = new Batch();

        int _currentTextureId = -1;
        public void DrawSprite(Sprite sprite)
        {
            if (sprite.Texture.Id == _currentTextureId)
            {
                _batch.AddSprite(sprite);
            }
            else
            {
                _batch.Draw(); // Draw all with current texture

                // Update texture info
                _currentTextureId = sprite.Texture.Id;
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, _currentTextureId);
                _batch.AddSprite(sprite);
            }
        }

        public void Render()
        {
            _batch.Draw();
        }

        public void DrawText(Text text)
        {
            foreach (CharacterSprite c in text.CharacterSprites)
            {
                DrawSprite(c.Sprite);
            }
        }
    }
}
