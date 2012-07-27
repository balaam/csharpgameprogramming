using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter8_9
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

        public static double Linear(double timePassed, double start, double distance, double duration)
        {
            return distance * timePassed / duration + start;
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

    }

}
