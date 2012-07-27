using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
namespace Chapter11_4
{
    class TileMapState : IGameObject
    {
        double _startX = 0;
        double _startY = 0;
        double _tileWidthHeight = 32;
        Renderer _renderer = new Renderer();

        Dictionary<char, TileData> _tileLookUp;
        TileMap _tileMap = new TileMap();
        TextureManager _textureManager;

        const string levelDef =
            "############\n" +
            "#          #\n" +
            "#          #\n" +
            "#S        E#\n" +
            "############\n";

        public TileMapState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _tileLookUp = LoadTileDefinitions();

            double currentX = _startX;
            double currentY = _startY;
            int xPosition = 0;
            int yPosition = 0;
            foreach (char c in levelDef)
            {
                if (c == '\n')
                {
                    xPosition = 0;
                    yPosition = yPosition + 1;
                    currentX = _startX;
                    currentY -= _tileWidthHeight;
                    continue;
                }

                Tile t = _tileLookUp[c].CreateTile();
                t.SetPosition(currentX, currentY);
                _tileMap.AddTile(xPosition, yPosition, t);

                xPosition++;
                currentX += _tileWidthHeight;
            }

        }

        private Dictionary<char, TileData> LoadTileDefinitions()
        {
            var tileDef = new Dictionary<char, TileData>();

            tileDef.Add(' ', new TileData(_textureManager.Get("sky")));
            tileDef.Add('#', new TileData(_textureManager.Get("wall")));
            tileDef.Add('S', new TileData(_textureManager.Get("start")));
            tileDef.Add('E', new TileData(_textureManager.Get("finish")));

            return tileDef;
        }

        public void Update(double elapsedTime)
        {

        }

        public void Render()
        {
            _tileMap.Render(_renderer);
            _renderer.Render();
        }
    }
}
