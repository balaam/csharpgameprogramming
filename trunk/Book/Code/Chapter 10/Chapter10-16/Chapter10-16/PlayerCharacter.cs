using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter10_16
{

    public class PlayerCharacter : Entity
    {
        bool _dead = false;
        BulletManager _bulletManager;
        Texture _bulletTexture;

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

        public PlayerCharacter(TextureManager textureManager, BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
            _bulletTexture = textureManager.Get("bullet");

            _sprite.Texture = textureManager.Get("player_ship");
            _sprite.SetScale(0.5, 0.5); // spaceship is quite big, scale it down.
        }





        public void Render(Renderer renderer)
        {
           // Render_Debug();
            renderer.DrawSprite(_sprite);
        }

        Vector _gunOffset = new Vector(55, 0, 0);
        static readonly double FireRecovery = 0.25;
        double _fireRecoveryTime = FireRecovery;
        public void Update(double elapsedTime)
        {
            _fireRecoveryTime = Math.Max(0, (_fireRecoveryTime - elapsedTime));
        }

        public void Fire()
        {
            if (_fireRecoveryTime > 0)
            {
                return;
            }
            else
            {
                _fireRecoveryTime = FireRecovery;
            }

            Bullet bullet = new Bullet(_bulletTexture);
            bullet.SetColor(new Color(0, 1, 0, 1));
            bullet.SetPosition(_sprite.GetPosition() + _gunOffset);
            _bulletManager.Shoot(bullet);
        }

        internal void OnCollision(Enemy enemy)
        {
            _dead = true;
        }

        internal void OnCollision(Bullet bullet)
        {
            _dead = true;
        }

        internal Vector GetPosition()
        {
            return _sprite.GetPosition();
        }
    }
}

