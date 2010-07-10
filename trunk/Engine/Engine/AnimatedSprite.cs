using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class AnimatedSprite : Sprite
    {
        int _framesX;
        int _framesY;
        int _currentFrame = 0;
        double _currentFrameTime = 0.03;
        public double Speed { get; set; } // seconds per frame
        public bool Looping { get; set; }
        public bool Finished { get; set; }

        public AnimatedSprite()
        {
            Looping = false;
            Finished = false;
            Speed = 0.03; // 30 fps-ish
            _currentFrameTime = Speed;
        }

        public System.Drawing.Point GetIndexFromFrame(int frame)
        {
            System.Drawing.Point point = new System.Drawing.Point();
            point.Y = frame / _framesX;
            point.X = frame - (point.Y * _framesY);
            return point;
        }

        private void UpdateUVs()
        {
            System.Drawing.Point index = GetIndexFromFrame(_currentFrame);
            float frameWidth = 1.0f / (float)_framesX;
            float frameHeight = 1.0f / (float)_framesY;
            SetUVs(new Point(index.X * frameWidth, index.Y * frameHeight),
                new Point((index.X + 1) * frameWidth, (index.Y + 1) * frameHeight));
        }

        public void SetAnimation(int framesX, int framesY)
        {
            _framesX = framesX;
            _framesY = framesY;
            UpdateUVs();
        }

        private int GetFrameCount()
        {
            return _framesX * _framesY;
        }

        public void AdvanceFrame()
        {
            int numberOfFrames = GetFrameCount();
            _currentFrame = (_currentFrame + 1) % numberOfFrames;
        }

        public int GetCurrentFrame()
        {
            return _currentFrame;
        }

        public void Process(double elapsedTime)
        {
            if (_currentFrame == GetFrameCount() - 1 && Looping == false)
            {
                Finished = true;
                return;
            }

            _currentFrameTime -= elapsedTime;
            if (_currentFrameTime < 0)
            {
                AdvanceFrame();
                _currentFrameTime = Speed;
                UpdateUVs();
            }
        }
    }

}
