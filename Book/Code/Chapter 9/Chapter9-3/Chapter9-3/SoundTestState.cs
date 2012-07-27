using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
namespace Chapter9_3
{
    class SoundTestState : IGameObject
    {
        SoundManager _soundManager;
        double _count = 3;

        public SoundTestState(SoundManager soundManager)
        {
            _soundManager = soundManager;

            //_soundManager.MasterVolume(1.0f);
        }

        public void Render()
        {
        }

        public void Update(double elapsedTime)
        {
            _count -= elapsedTime;

            if (_count < 0)
            {
                _count = 3;
                _soundManager.PlaySound("effect");

                // Uncomment this line to hear both sides play at once.
                //_soundManager.PlaySound("effect2");
            }


            // Comment out the above code and uncomment the below
            // The below code checks that the StopSound and IsSoundPlaying functiond work correctly.
            //_count -= elapsedTime;

            //if (_count < 0)
            //{
            //    _count = 3;
            //    Sound soundOne = _soundManager.PlaySound("effect");
            //    Sound soundTwo = _soundManager.PlaySound("effect2");

            //    if (_soundManager.IsSoundPlaying(soundOne))
            //    {
            //        _soundManager.StopSound(soundOne);
            //    }
            //}

        }
    }
}
