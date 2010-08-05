using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class Tween
    {
        double _original;
        double _distance;
        double _current;
        double _totalTimePassed = 0;
        double _totalDuration = 5;
        bool _finished = false;
        TweenFunction _tweenF = null;
        public delegate double TweenFunction(double timePassed, double start, double distance, double duration);

        public double Value()
        {
            return _current;
        }

        public bool IsFinished()
        {
            return _finished;
        }


        public Tween(double start, double end, double time)
        {
            Construct(start, end, time, Tween.Linear);
        }

        public Tween(double start, double end, double time, TweenFunction tweenF)
        {
            Construct(start, end, time, tweenF);
        }

        public void Construct(double start, double end, double time, TweenFunction tweenF)
        {
            _distance = end - start;
            _original = start;
            _current = start;
            _totalDuration = time;
            _tweenF = tweenF;
        }

        public void Update(double elapsedTime)
        {
            _totalTimePassed += elapsedTime;
            _current = _tweenF(_totalTimePassed, _original, _distance, _totalDuration);

            if (_totalTimePassed > _totalDuration)
            {
                _current = _original + _distance;
                _finished = true;
            }
        }


        public static double Linear(double timePassed, double start, double distance, double duration)
        {
            return distance * timePassed / duration + start;
        }

        public static double EaseOutExpo(double timePassed, double start, double distance, double duration)
        {
            if (timePassed == duration)
            {
                return start + distance;
            }
            return distance * (-Math.Pow(2, -10 * timePassed / duration) + 1) + start;
        }

        public static double EaseInExpo(double timePassed, double start, double distance, double duration)
        {
            if (timePassed == 0)
            {
                return start;
            }
            else
            {
                return distance * Math.Pow(2, 10 * (timePassed / duration - 1)) + start;
            }
        }

        public static double EaseOutCirc(double timePassed, double start, double distance, double duration)
        {
            return distance * Math.Sqrt(1 - (timePassed = timePassed / duration - 1) * timePassed) + start;
        }

        public static double EaseInCirc(double timePassed, double start, double distance, double duration)
        {
            return -distance * (Math.Sqrt(1 - (timePassed /= duration) * timePassed) - 1) + start;
        }

        public static double BounceEaseOut(double timePassed, double start, double distance, double duration)
        {
            if ((timePassed /= duration) < (1 / 2.75))
                return distance * (7.5625 * timePassed * timePassed) + start;
            else if (timePassed < (2 / 2.75))
                return distance * (7.5625 * (timePassed -= (1.5 / 2.75)) * timePassed + .75) + start;
            else if (timePassed < (2.5 / 2.75))
                return distance * (7.5625 * (timePassed -= (2.25 / 2.75)) * timePassed + .9375) + start;
            else
                return distance * (7.5625 * (timePassed -= (2.625 / 2.75)) * timePassed + .984375) + start;
        }

        public static double BounceEaseIn(double timePassed, double start, double distance, double duration)
        {
            return distance - BounceEaseOut(duration - timePassed, 0, distance, duration) + start;
        }

        public static double BounceEaseInOut(double timePassed, double start, double distance, double duration)
        {
            if (timePassed < duration / 2)
                return BounceEaseIn(timePassed * 2, 0, distance, duration) * .5 + start;
            else
                return BounceEaseOut(timePassed * 2 - duration, 0, distance, duration) * .5 + distance * .5 + start;
        }


    }

}
