using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter9_2
{
    class MultipleTexturesState : IGameObject
    {
        Sprite _spaceship1 = new Sprite();
        Sprite _spaceship2 = new Sprite();
        Renderer _renderer = new Renderer();

        public MultipleTexturesState(TextureManager textureManager)
        {
            _spaceship1.Texture = textureManager.Get("spaceship");
            _spaceship2.Texture = textureManager.Get("spaceship2");

            // Move the first spaceship, so they're not overlapping.
            _spaceship1.SetPosition(-300, 0);
        }

        public void Update(double elapsedTime) { }

        public void Render()
        {
            _renderer.DrawSprite(_spaceship1);
            _renderer.DrawSprite(_spaceship2);
            _renderer.Render();
        }
    }
}
