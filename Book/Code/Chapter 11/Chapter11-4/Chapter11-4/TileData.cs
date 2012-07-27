using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
namespace Chapter11_4
{
    class TileData
    {
        Texture _texture;
        public TileData(Texture texture)
        {
            _texture = texture;
        }
        internal Tile CreateTile()
        {
            Sprite sprite = new Sprite();
            sprite.Texture = _texture;
            Tile tile = new Tile(sprite);
            return tile;
        }
    }
}
