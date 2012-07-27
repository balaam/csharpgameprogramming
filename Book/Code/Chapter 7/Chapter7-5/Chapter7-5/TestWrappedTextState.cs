using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter7_5
{
    class TestWrappedTextState : IGameObject
    {
        TextureManager _textureManager;
        Font _font;
        Text _longText;
        Renderer _renderer = new Renderer();

        public TestWrappedTextState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _font = new Font(textureManager.Get("font"),
                FontParser.Parse("font.fnt"));
            _longText = new Text("The quick brown fox jumps over the lazy dog",
           _font, 400);
            

        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawText(_longText);
            _renderer.Render(); // new batch method
        }

        public void Update(double elapsedTime)
        {
        }
    }
}
