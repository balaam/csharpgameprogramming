using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter8_4
{
    class CharacterBounceState : IGameObject
    {
         Font _font;
        Text _text;
        Renderer _renderer = new Renderer();
        double _totalTime = 0;

        public CharacterBounceState(TextureManager manager)
        {
            _font = new Font(manager.Get("font"), FontParser.Parse("font.fnt"));
            _text = new Text("Hello", _font);
        }

        public void Update(double elapsedTime)
        {
            double frequency = 7;

            int xAdvance = 0;
            foreach (CharacterSprite cs in _text.CharacterSprites)
            {
                Vector position = cs.Sprite.GetPosition();
                position.Y = 0 + Math.Sin((_totalTime + xAdvance) * frequency) * 25;
                cs.Sprite.SetPosition(position);
                xAdvance++;
            }

            _totalTime += elapsedTime;

        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawText(_text);
            _renderer.Render();
        }
    }
}
