using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter10_8
{

    class PlayerCharacter : Entity
    {
        bool _dead = false;
        public bool IsDead
        {
            get
            {
                return _dead;
            }
        }

        double _speed = 512; // pixels per second

        public void Move(Vector amount)
        {
            amount *= _speed;
            _sprite.SetPosition(_sprite.GetPosition() + amount);
        }

        public PlayerCharacter(TextureManager textureManager)
        {
            _sprite.Texture = textureManager.Get("player_ship");
            _sprite.SetScale(0.5, 0.5); // spaceship is quite big, scale it down.
        }

        public void Render(Renderer renderer)
        {
            Render_Debug();
            renderer.DrawSprite(_sprite);
        }

        internal void OnCollision(Enemy enemy)
        {
            _dead = true;
        }
    }
}
