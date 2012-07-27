using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using System.Drawing;
using Tao.OpenGl;

namespace Chapter10_8
{
    public class Enemy : Entity
    {
        double _scale = 0.3;
        public Enemy(TextureManager textureManager)
    {
        _sprite.Texture = textureManager.Get("enemy_ship");
        _sprite.SetScale(_scale, _scale);
        _sprite.SetRotation(Math.PI); // make it face the player
        _sprite.SetPosition(200, 0); // put it somewhere easy to see
    }

        public void Update(double elapsedTime)
        {
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

    }

}
