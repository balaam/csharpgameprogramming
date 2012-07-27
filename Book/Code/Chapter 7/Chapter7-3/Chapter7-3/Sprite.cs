using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter7_3
{
    public class Sprite
    {
        internal const int VertexAmount = 6;
        Vector[] _vertexPositions = new Vector[VertexAmount];
        Color[] _vertexColors = new Color[VertexAmount];
        Point[] _vertexUVs = new Point[VertexAmount];
        Texture _texture = new Texture();

        public Sprite()
        {
            InitVertexPositions(new Vector(0, 0, 0), 1, 1);
            SetColor(new Color(1, 1, 1, 1));
            SetUVs(new Point(0, 0), new Point(1, 1));
        }

        public Texture Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                // By default the width and height is set
                // to that of the texture
                InitVertexPositions(GetCenter(), _texture.Width, _texture.Height);
            }
        }

        public Vector[] VertexPositions
        {
            get { return _vertexPositions; }
        }

        public Color[] VertexColors
        {
            get { return _vertexColors; }
        }

        public Point[] VertexUVs
        {
            get { return _vertexUVs; }
        }

        private Vector GetCenter()
        {
            double halfWidth = GetWidth() / 2;
            double halfHeight = GetHeight() / 2;


            return new Vector(
                _vertexPositions[0].X + halfWidth,
                _vertexPositions[0].Y - halfHeight,
                _vertexPositions[0].Z);
        }

        private void InitVertexPositions(Vector position, double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            // Clockwise creation of two triangles to make a quad.

            // TopLeft, TopRight, BottomLeft
            _vertexPositions[0] = new Vector(position.X - halfWidth, position.Y + halfHeight, position.Z);
            _vertexPositions[1] = new Vector(position.X + halfWidth, position.Y + halfHeight, position.Z);
            _vertexPositions[2] = new Vector(position.X - halfWidth, position.Y - halfHeight, position.Z);

            // TopRight, BottomRight, BottomLeft
            _vertexPositions[3] = new Vector(position.X + halfWidth, position.Y + halfHeight, position.Z);
            _vertexPositions[4] = new Vector(position.X + halfWidth, position.Y - halfHeight, position.Z);
            _vertexPositions[5] = new Vector(position.X - halfWidth, position.Y - halfHeight, position.Z);
        }

        public double GetWidth()
        {
            // topright - topleft 
            return _vertexPositions[1].X - _vertexPositions[0].X;
        }

        public double GetHeight()
        {
            // topleft - bottomleft
            return _vertexPositions[0].Y - _vertexPositions[2].Y;
        }

        public void SetWidth(float width)
        {
            InitVertexPositions(GetCenter(), width, GetHeight());
        }

        public void SetHeight(float height)
        {
            InitVertexPositions(GetCenter(), GetWidth(), height);
        }

        public void SetPosition(double x, double y)
        {
            SetPosition(new Vector(x, y, 0));
        }

        public void SetPosition(Vector position)
        {
            InitVertexPositions(position, GetWidth(), GetHeight());
        }

        public void SetColor(Color color)
        {
            for (int i = 0; i < Sprite.VertexAmount; i++)
            {
                _vertexColors[i] = color;
            }
        }

        public void SetUVs(Point topLeft, Point bottomRight)
        {
            // TopLeft, TopRight, BottomLeft
            _vertexUVs[0] = topLeft;
            _vertexUVs[1] = new Point(bottomRight.X, topLeft.Y);
            _vertexUVs[2] = new Point(topLeft.X, bottomRight.Y);

            // TopRight, BottomRight, BottomLeft
            _vertexUVs[3] = new Point(bottomRight.X, topLeft.Y);
            _vertexUVs[4] = bottomRight;
            _vertexUVs[5] = new Point(topLeft.X, bottomRight.Y);

        }

    }
}
