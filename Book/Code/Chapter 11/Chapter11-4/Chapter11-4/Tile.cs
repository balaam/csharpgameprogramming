using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter11_4
{
    class Tile
    {
        Sprite _sprite;
        public Tile(Sprite sprite)
        {
            _sprite = sprite;
        }
        internal void SetPosition(double currentX, double currentY)
        {
            _sprite.SetPosition(currentX, currentY);
        }

        internal void Render(Renderer renderer)
        {
            renderer.DrawSprite(_sprite);
        }
    }
}
