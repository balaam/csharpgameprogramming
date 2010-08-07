using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Editor
{
    // Could be an abstract class, for now just a sprite.
    public class Layer
    {
        string _name;
        Sprite _sprite = new Sprite();
        public Layer(string layerName)
        {
            _name = layerName;
        }

        public string Name
        {
            get { return _name; }
        }
        public string TextureId
        {
            // This isn't generally correct but it is  for this adventure game project
            // The texture manager uses the path as the key.
            get { return _sprite.Texture.Path; }
        }

        public void SetImage(Texture texture)
        {
            _sprite.Texture = texture;
        }

        public void Render(Renderer renderer)
        {
            renderer.DrawSprite(_sprite);
        }

        public void Update(double elapsedTime)
        {
            //throw new NotImplementedException();
        }
    }
}
