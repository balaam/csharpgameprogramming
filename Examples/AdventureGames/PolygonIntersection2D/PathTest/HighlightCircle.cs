using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace WalkablePolygonCodeSketch.PathTest
{
    class HighlightCircle
    {
        double _maxRadius = 15;
        Circle _circle;
        Tween _highlightTween;
        bool _mouseInside = false;
        bool _focus = false;

        public bool Focus
        {
            get
            {
                return _focus;
            }
        }

        public HighlightCircle(Point position)
        {
             _circle = new Circle(position.X, position.Y, _maxRadius);
             _highlightTween = new Tween(0, 0, 0);
        }

        public void Update(double elapsedTime, Point mousePosition)
        {
            _highlightTween.Update(elapsedTime);

            var oldRadius = _circle.Radius;

            _circle.Radius = _maxRadius;
            // use to decide how to render the circle
            if (_circle.Intersects(mousePosition))
            {
                if (_mouseInside == false)
                {
                    _mouseInside = true;
                    OnGainFocus();
                }
            }
            else
            {
                if (_mouseInside)
                {
                    _mouseInside = false;
                    OnLoseFocus();
                }
            }
            _circle.Radius = oldRadius;
        }

        private void OnGainFocus()
        {
            _focus = true;
            _highlightTween = new Tween(_highlightTween.Value(), _maxRadius, 0.3);
        }

        private void OnLoseFocus()
        {
            _focus = false;
            _highlightTween = new Tween(_highlightTween.Value(), 0, 0.3);
        }

        public void Render()
        {
            _circle.Radius = _highlightTween.Value();
            PrimitiveDrawer.DrawDashedCircle(_circle, new Color(1, 0, 0, 1));
        }
    }
}
