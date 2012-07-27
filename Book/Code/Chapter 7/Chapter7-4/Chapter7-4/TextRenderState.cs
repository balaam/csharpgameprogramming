using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter7_4
{
    class TextRenderState : IGameObject
    {
        TextureManager _textureManager;
        Font _font;
        Text _helloWorld;
        Renderer _renderer = new Renderer();

        public TextRenderState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _font = new Font(textureManager.Get("font"),
                FontParser.Parse("font.fnt"));
            _helloWorld = new Text("Hello", _font);
        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawText(_helloWorld);
        }

        public void Update(double elapsedTime)
        {
        }
    }

}
