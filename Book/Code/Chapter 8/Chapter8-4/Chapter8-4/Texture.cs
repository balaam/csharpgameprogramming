
namespace Chapter8_4
{
    public struct Texture
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Texture(int id, int width, int height)
            : this()
        {
            Id = id;
            Width = width;
            Height = height;
        }
    }
}
