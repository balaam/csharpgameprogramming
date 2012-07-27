using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter10_9
{
    public class Bullet : Entity
    {
        public bool Dead { get; set; }
        public Vector Direction { get; set; }
        public double Speed { get; set; }

        public double X
        {
            get { return _sprite.GetPosition().X; }
        }

        public double Y
        {
            get { return _sprite.GetPosition().Y; }
        }

        public void SetPosition(Vector position)
        {
            _sprite.SetPosition(position);
        }

        public void SetColor(Color color)
        {
            _sprite.SetColor(color);
        }

        public Bullet(Texture bulletTexture)
        {
            _sprite.Texture = bulletTexture;

            // Some default values
            Dead = false;
            Direction = new Vector(1, 0, 0);
            Speed = 512;// pixels per second
        }
        public void Render(Renderer renderer)
        {
            if (Dead)
            {
                return;
            }
            renderer.DrawSprite(_sprite);
        }

        public void Update(double elapsedTime)
        {
            if (Dead)
            {
                return;
            }
            Vector position = _sprite.GetPosition();
            position += Direction * Speed * elapsedTime;
            _sprite.SetPosition(position);
        }
    }

}

