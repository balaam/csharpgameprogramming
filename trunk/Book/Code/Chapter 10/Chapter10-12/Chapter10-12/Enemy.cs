using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using System.Drawing;
using Tao.OpenGl;

namespace Chapter10_12
{
    public class Enemy : Entity
    {
        static readonly double HitFlashTime = 0.25;
        double _scale = 0.3;
        public int Health { get; set; }
        double _hitFlashCountDown = 0;

        EffectsManager _effectsManager;

        public bool IsDead
        {
            get { return Health == 0; }
        }



        public Enemy(TextureManager textureManager, EffectsManager effectsManager)
        {
            _effectsManager = effectsManager;
            Health = 50; // default health value.

            _sprite.Texture = textureManager.Get("enemy_ship");
            _sprite.SetScale(_scale, _scale);
            _sprite.SetRotation(Math.PI); // make it face the player
            _sprite.SetPosition(200, 0); // put it somewhere easy to see
        }

        public void Update(double elapsedTime)
        {
            if (_hitFlashCountDown != 0)
            {
                _hitFlashCountDown = Math.Max(0, _hitFlashCountDown - elapsedTime);
                double scaledTime = 1 - (_hitFlashCountDown / HitFlashTime);
                _sprite.SetColor(new Engine.Color(1, 1, (float)scaledTime, 1));
            }

        }

        public void Render(Renderer renderer)
        {
            renderer.DrawSprite(_sprite);
            Render_Debug();
        }

        internal void OnCollision(PlayerCharacter player)
        {
            // Handle collision with player.
        }


        internal void OnCollision(Bullet bullet)
        {
            // If the ship is already dead then ignore any more bullets.
            if (Health == 0)
            {
                return;
            }

            Health = Math.Max(0, Health - 25);
            _hitFlashCountDown = HitFlashTime; // half
            _sprite.SetColor(new Engine.Color(1, 1, 0, 1));

            if (Health == 0)
            {
                OnDestroyed();
            }

        }

        private void OnDestroyed()
        {
            _effectsManager.AddExplosion(_sprite.GetPosition());
        }

        internal void SetPosition(Vector position)
        {
            _sprite.SetPosition(position);
        }
    }

}
