using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Tao.OpenGl;

namespace WalkablePolygonCodeSketch
{
    class PolygonPlotter
    {


        List<PolyVertex> _vertexList = new List<PolyVertex>();

        // Bring up a dashed circle when the player moves the mouse near the first vertex
        bool _mouseNearFirstVertex = false;
        Tween _highlightCircleTween = new Tween(0, 0, 0);
        Circle _hightLightCircle = new Circle(0, 0, 15);

        public Polygon AddPoint(Point point)
        {
            if (_mouseNearFirstVertex)
            {
                // Special case on completing the polygon.
             //   _vertexList.Add(_vertexList.First());
                Polygon p = new Polygon(_vertexList);
                _vertexList.Clear();
                // reset the highlight
                 _mouseNearFirstVertex = false;
                 _highlightCircleTween = new Tween(0, 0, 0);
                return p;
            }
            else
            {
                _vertexList.Add(new PolyVertex(point, new Tween(0, 10, 0.5, Tween.BounceEaseOut)));
            }
            return null;
        }

        public void Update(double elapsedTime, Point mousePosition)
        {
            HighlightFirstVertex(elapsedTime, mousePosition.X, mousePosition.Y);

            foreach (PolyVertex v in _vertexList)
            {
                v.Tween.Update(elapsedTime);
            }
        }

        private void HighlightFirstVertex(double elapsedTime, float mouseX, float mouseY)
        {
            PolyVertex firstPoint = _vertexList.FirstOrDefault();
            _highlightCircleTween.Update(elapsedTime);
            _hightLightCircle.Radius = _highlightCircleTween.Value();
            if (firstPoint == null)
            {
                return;
            }

            _hightLightCircle.X = firstPoint.Point.X;
            _hightLightCircle.Y = firstPoint.Point.Y;
            _hightLightCircle.Radius = 15; // set to max for intersection test
            if (_hightLightCircle.Intersects(mouseX, mouseY))
            {
                if (_mouseNearFirstVertex == false)
                {
                    _highlightCircleTween = new Tween(_highlightCircleTween.Value(), 15, 0.4);
                    _mouseNearFirstVertex = true;
                }
            }
            else if (_mouseNearFirstVertex)
            {
                _mouseNearFirstVertex = false;
                _highlightCircleTween = new Tween(_highlightCircleTween.Value(), 0, 0.4);
            }
            _hightLightCircle.Radius = _highlightCircleTween.Value();
        }

        public void Render()
        {
            foreach (PolyVertex v in _vertexList)
            {
                PrimitiveDrawer.DrawSquare(v.Point.X, v.Point.Y, v.Tween.Value(), new Color(1, 0, 0, 1));
            }

            PrimitiveDrawer.DrawDashedCircle(_hightLightCircle, new Color(1, 0, 0, 1));

            Gl.glBegin(Gl.GL_LINE_STRIP);
            {
                foreach (PolyVertex v in _vertexList)
                {
                    GLUtil.DrawPointVertex(v.Point);
                }
            }
            Gl.glEnd();
        }
    }
}
