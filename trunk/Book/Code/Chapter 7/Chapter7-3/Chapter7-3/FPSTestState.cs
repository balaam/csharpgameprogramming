using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter7_3
{
    class FPSTestState : IGameObject
    {
        TextureManager _textureManager;
        Font _font;
        Text _fpsText;
        Renderer _renderer = new Renderer();
        FramesPerSecond _fps = new FramesPerSecond();

        public FPSTestState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _font = new Font(textureManager.Get("font"),
                FontParser.Parse("font.fnt"));
            _fpsText = new Text("FPS:", _font);
        }

        #region IGameObject Members
        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _fpsText = new Text("FPS: " + _fps.CurrentFPS.ToString("00.0"), _font);
            _renderer.DrawText(_fpsText);

            // Uncomment this to render around 10,000 quads
            // Note the FPS change.
            //for (int i = 0; i < 1000; i++)
            //{
            //   _renderer.DrawText(_fpsText);
            //}

        }

        public void Update(double elapsedTime)
        {
            _fps.Process(elapsedTime);
        }
        #endregion
    }

}
