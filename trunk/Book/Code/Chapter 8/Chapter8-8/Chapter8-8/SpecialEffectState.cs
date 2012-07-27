using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter8_8
{
    class SpecialEffectState : IGameObject
    {
        Font _font;
        Text _text;
        Renderer _renderer = new Renderer();
        double _totalTime = 0;

        public SpecialEffectState(TextureManager manager)
        {
            _font = new Font(manager.Get("font"), FontParser.Parse("font.fnt"));
            _text = new Text("Hello", _font);
        }

        public void Update(double elapsedTime)
        {
            double frequency = 7;
            float _wavyNumber = (float)Math.Sin(_totalTime * frequency);
            _wavyNumber = 0.5f + _wavyNumber * 0.5f; // scale to 0-1

            _text.SetColor(new Color(1, 0, 0, _wavyNumber));

            _totalTime += elapsedTime;

            // Comment out the above code and remove the comments from the below
            // code to see a rainbow effect applied to the text.
            //double frequency = 7;
            //float _wavyNumberR = (float)Math.Sin(_totalTime * frequency);
            //float _wavyNumberG = (float)Math.Cos(_totalTime * frequency);
            //float _wavyNumberB = (float)Math.Sin(_totalTime + 0.25 * frequency);
            //_wavyNumberR = 0.5f + _wavyNumberR * 0.5f; // scale to 0-1
            //_wavyNumberG = 0.5f + _wavyNumberG * 0.5f; // scale to 0-1
            //_wavyNumberB = 0.5f + _wavyNumberB * 0.5f; // scale to 0-1

            //_text.SetColor(new Color(_wavyNumberR, _wavyNumberG, _wavyNumberB, 1));

            //_totalTime += elapsedTime;

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
