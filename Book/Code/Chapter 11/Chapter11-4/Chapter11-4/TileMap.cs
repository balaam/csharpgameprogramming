using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11_4
{
    class TileMap
    {
        struct MapPosition
        {
            public int X { get; set; }
            public int Y { get; set; }

           
        }
        Dictionary<MapPosition, Tile> _map = new Dictionary<MapPosition,Tile>();
        public TileMap()
        {

        }

        internal void AddTile(int xPosition, int yPosition, Tile t)
        {
            _map.Add(new MapPosition { X = xPosition, Y = yPosition }, t);
        }

        internal void Render(Engine.Renderer renderer)
        {
            foreach (Tile t in _map.Values)
            {
                t.Render(renderer);
            }
        }
    }
}
