using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter10_13
{
    public class EffectsManager
    {
        List<AnimatedSprite> _effects = new List<AnimatedSprite>();
        TextureManager _textureManager;

        public EffectsManager(TextureManager textureManager)
        {
            _textureManager = textureManager;
        }

        public void AddExplosion(Vector position)
        {
            AnimatedSprite explosion = new AnimatedSprite();
            explosion.Texture = _textureManager.Get("explosion");
            explosion.SetAnimation(4, 4);
            explosion.SetPosition(position);
            _effects.Add(explosion);
        }

        public void Update(double elapsedTime)
        {
            _effects.ForEach(x => x.Process(elapsedTime));
            RemoveDeadExplosions();
        }

        public void Render(Renderer renderer)
        {
            _effects.ForEach(x => renderer.DrawSprite(x));
        }

        private void RemoveDeadExplosions()
        {
            for (int i = _effects.Count - 1; i >= 0; i--)
            {
                if (_effects[i].Finished)
                {
                    _effects.RemoveAt(i);
                }
            }
        }

    }
}
