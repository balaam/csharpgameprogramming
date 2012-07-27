using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter10_6
{
    class PlayerCharacter
    {

        Sprite _spaceship = new Sprite();
        double _speed = 512; // pixels per second

        public PlayerCharacter(TextureManager textureManager)
        {
            _spaceship.Texture = textureManager.Get("player_ship");
            _spaceship.SetScale(0.5, 0.5); // spaceship is quite big, scale it down.
        }

        public void Render(Renderer renderer)
        {
            renderer.DrawSprite(_spaceship);
        }

       

        public void Move(Vector amount)
        {
            amount *= _speed;
            _spaceship.SetPosition(_spaceship.GetPosition() + amount);
        }


    }
}
