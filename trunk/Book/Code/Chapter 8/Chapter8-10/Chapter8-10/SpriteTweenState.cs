using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Chapter8_10
{

    class SpriteTweenState : IGameObject
    {
        Sprite _faceSprite = new Sprite();
        Renderer _renderer = new Renderer();

        // Try altering the tween functions for different effects.
        Tween _tween = new Tween(0, 256, 5);
        Tween _alphaTween = new Tween(0, 1, 5, Tween.EaseInCirc); 
        Color _color = new Color(1, 1, 1, 0);


        public SpriteTweenState(TextureManager textureManager)
        {
            _faceSprite.Texture = textureManager.Get("face");
            _faceSprite.SetHeight(0);
            _faceSprite.SetWidth(0);
        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawSprite(_faceSprite);
            _renderer.Render();
        }

        public void Update(double elapsedTime)
        {
            if (_tween.IsFinished() != true)
            {
                _tween.Update(elapsedTime);
                _faceSprite.SetWidth((float)_tween.Value());
                _faceSprite.SetHeight((float)_tween.Value());
            }

            // This additional alpha tween has been added.
            if (_alphaTween.IsFinished() != true)
            {
                _alphaTween.Update(elapsedTime);
                _color.Alpha = (float)_alphaTween.Value();
                _faceSprite.SetColor(_color);
            }

        }


    
    }
}
