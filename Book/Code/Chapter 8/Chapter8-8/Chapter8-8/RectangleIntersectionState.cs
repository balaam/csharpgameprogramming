using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Chapter8_8
{
    class RectangleIntersectionState : IGameObject
    {
        Input _input;
        Rectangle _rectangle = new Rectangle(new Vector(0, 0, 0), new Vector(200, 200, 0));
        public RectangleIntersectionState(Input input)
        {
            _input = input;
        }

        #region IGameObject Members

        public void Update(double elapsedTime)
        {
            if (_rectangle.Intersects(_input.MousePosition))
            {
                _rectangle.Color = new Color(1, 0, 0, 1);
            }
            else
            {
                // If the circle’s not intersected turn it back to white.
                _rectangle.Color = new Color(1, 1, 1, 1);
            }
        }

        public void Render()
        {
            _rectangle.Render();
        }
        #endregion
    }

}
