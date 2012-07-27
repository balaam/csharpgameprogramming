using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter7_3
{

    public class Font
    {
        Texture _texture;
        Dictionary<char, CharacterData> _characterData;

        public Font(Texture texture, Dictionary<char, CharacterData> characterData)
        {
            _texture = texture;
            _characterData = characterData;
        }

        public CharacterSprite CreateSprite(char c)
        {
            CharacterData charData = _characterData[c];
            Sprite sprite = new Sprite();
            sprite.Texture = _texture;

            // Setup UVs
            Point topLeft = new Point((float)charData.X / (float)_texture.Width,
                                        (float)charData.Y / (float)_texture.Height);
            Point bottomRight = new Point(topLeft.X + ((float)charData.Width / (float)_texture.Width),
                                          topLeft.Y + ((float)charData.Height / (float)_texture.Height));
            sprite.SetUVs(topLeft, bottomRight);
            sprite.SetWidth(charData.Width);
            sprite.SetHeight(charData.Height);
            sprite.SetColor(new Color(1, 1, 1, 1));

            return new CharacterSprite(sprite, charData);
        }
    }


}
