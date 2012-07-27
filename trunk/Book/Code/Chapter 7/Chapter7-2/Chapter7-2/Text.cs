using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter7_2
{
    public class Text
    {
        Font _font;
        List<CharacterSprite> _bitmapText = new List<CharacterSprite>();
        string _text;

        public List<CharacterSprite> CharacterSprites
        {
            get { return _bitmapText; }
        }

        public Text(string text, Font font)
        {
            _text = text;
            _font = font;

            CreateText(0, 0);
        }

        private void CreateText(double x, double y)
        {
            _bitmapText.Clear();
            double currentX = x;
            double currentY = y;

            // Kerning should go here, use char previousCharacter = nil
            foreach (char c in _text)
            {
                CharacterSprite sprite = _font.CreateSprite(c);
                float xOffset = ((float)sprite.Data.XOffset) / 2;
                float yOffset = ((float)sprite.Data.YOffset) / 2;
                sprite.Sprite.SetPosition(currentX + xOffset, currentY - yOffset);
                currentX += sprite.Data.XAdvance;
                _bitmapText.Add(sprite);
            }
        }

    }
}
