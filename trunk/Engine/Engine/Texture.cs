
namespace Engine
{
    public struct Texture
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Path { get; set; }

        public Texture(int id, int width, int height, string path)
            : this(id, width, height)
        {
            Path = path;
        }

        public Texture(int id, int width, int height)
            : this()
        {
            Id = id;
            Width = width;
            Height = height;
        }

    }
}
