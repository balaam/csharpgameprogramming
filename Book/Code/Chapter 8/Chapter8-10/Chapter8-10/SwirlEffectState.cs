using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter8_10
{
    class SwirlEffectState : IGameObject
    {
        Font _font;
        Text _text;
        Renderer _renderer = new Renderer();
        double _totalTime = 0;

        public SwirlEffectState(TextureManager manager)
        {
            _font = new Font(manager.Get("font"), FontParser.Parse("font.fnt"));
            _text = new Text("Hello", _font);
        }

        public void Update(double elapsedTime)
        {
            double frequency = 7;
            double _wavyNumberX = Math.Sin(_totalTime * frequency) * 15;
            double _wavyNumberY = Math.Cos(_totalTime * frequency) * 15;

            _text.SetPosition(_wavyNumberX, _wavyNumberY);

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
