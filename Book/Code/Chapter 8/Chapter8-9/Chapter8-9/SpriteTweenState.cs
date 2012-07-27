using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter8_9
{

    class SpriteTweenState : IGameObject
    {
        Tween _tween = new Tween(0, 256, 5);
        Sprite _sprite = new Sprite();
        Renderer _renderer = new Renderer();
        public SpriteTweenState(TextureManager textureManager)
        {
            _sprite.Texture = textureManager.Get("face");
            _sprite.SetHeight(0);
            _sprite.SetWidth(0);
        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawSprite(_sprite);
            _renderer.Render();

        }

        public void Update(double elapsedTime)
        {
            if (_tween.IsFinished() != true)
            {
                _tween.Update(elapsedTime);
                _sprite.SetWidth((float)_tween.Value());
                _sprite.SetHeight((float)_tween.Value());
            }
        }


    
    }
}
