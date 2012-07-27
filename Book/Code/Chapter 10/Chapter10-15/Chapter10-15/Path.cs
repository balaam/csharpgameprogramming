using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter10_15
{
    public class Path
    {

        Spline _spline = new Spline();
        Tween _tween;

        public Path(List<Vector> points, double travelTime)
        {
            foreach (Vector v in points)
            {
                _spline.AddPoint(v);
            }
            _tween = new Tween(0, 1, travelTime);
        }

        public void UpdatePosition(double elapsedTime, Enemy enemy)
        {
            _tween.Update(elapsedTime);
            Vector position = _spline.GetPositionOnLine(_tween.Value());
            enemy.SetPosition(position);
        }
        

    }
}
