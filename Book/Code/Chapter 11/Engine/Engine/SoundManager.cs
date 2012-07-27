using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenAl;
using System.IO;

namespace Engine
{
    public class SoundManager
    {
        struct SoundSource
        {
            public SoundSource(int bufferId, string filePath)
            {
                _bufferId = bufferId;
                _filePath = filePath;
            }
            public int _bufferId;
            string _filePath;
        }
        Dictionary<string, SoundSource> _soundIdentifier = new Dictionary<string, SoundSource>(); 
        readonly int MaxSoundChannels = 256;
        List<int> _soundChannels = new List<int>();
        float _masterVolume = 1.0f;

        public SoundManager()
        {
            Alut.alutInit();
            DicoverSoundChannels();
        }

        private void DicoverSoundChannels()
        {
            while (_soundChannels.Count < MaxSoundChannels)
            {
                int src;
                Al.alGenSources(1, out src);
                if (Al.alGetError() == Al.AL_NO_ERROR)
                {
                    _soundChannels.Add(src);
                }
                else
                {
                    break; // there's been an error - we've filled all the channels.
                }
            }
        }

        private bool IsChannelPlaying(int channel)
        {
            int value = 0;
            Al.alGetSourcei(channel, Al.AL_SOURCE_STATE, out value);
            return (value == Al.AL_PLAYING);
        }

        private int FindNextFreeChannel()
        {
            foreach (int slot in _soundChannels)
            {
                if (!IsChannelPlaying(slot))
                {
                    return slot;
                }
            }

            return -1;
        }


        public void LoadSound(string soundId, string path)
        {
            // Generate a buffer.
            int buffer = -1;
            Al.alGenBuffers(1, out buffer);

            int errorCode = Al.alGetError();
            System.Diagnostics.Debug.Assert(errorCode == Al.AL_NO_ERROR);

            int format;
            float frequency;
            int size;
            System.Diagnostics.Debug.Assert(File.Exists(path));
            IntPtr data = Alut.alutLoadMemoryFromFile(path, out format, out size, out frequency);
            System.Diagnostics.Debug.Assert(data != IntPtr.Zero);
            // Load wav data into the generated buffer.
            Al.alBufferData(buffer, format, data, size, (int)frequency);
            // Every seems ok, add it to the library.
            _soundIdentifier.Add(soundId, new SoundSource(buffer, path));
        }

        public Sound PlaySound(string soundId)
        {
            // Default play sound doesn't loop.
            return PlaySound(soundId, false);
        }
        public Sound PlaySound(string soundId, bool loop)
        {
            int channel = FindNextFreeChannel();
            if (channel != -1)
            {
                Al.alSourceStop(channel);
                Al.alSourcei(channel, Al.AL_BUFFER, _soundIdentifier[soundId]._bufferId);
                Al.alSourcef(channel, Al.AL_PITCH, 1.0f);
                Al.alSourcef(channel, Al.AL_GAIN, 1.0f);

                if (loop)
                {
                    Al.alSourcei(channel, Al.AL_LOOPING, 1);
                }
                else
                {
                    Al.alSourcei(channel, Al.AL_LOOPING, 0);
                }
              //  Al.alSourcef(channel, Al.AL_GAIN, _masterVolume);
                Al.alSourcePlay(channel);
                return new Sound(channel);
            }
            else
            {
                // Error sound
                return new Sound(-1);
            }
        }


        public void ChangeVolume(Sound sound, float value)
        {
            Al.alSourcef(sound.Channel, Al.AL_GAIN, _masterVolume * value);
        }

        public bool IsSoundPlaying(Sound sound)
        {
            return IsChannelPlaying(sound.Channel);
        }

        public void StopSound(Sound sound)
        {
            if (sound.Channel == -1)
            {
                return;
            }
            Al.alSourceStop(sound.Channel);
        }

        public void MasterVolume(float value)
        {
            _masterVolume = value;
            foreach (int channel in _soundChannels)
            {
                Al.alSourcef(channel, Al.AL_GAIN, value);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {

            foreach (SoundSource soundSource in _soundIdentifier.Values)
            {
                SoundSource temp = soundSource;
                Al.alDeleteBuffers(1, ref temp._bufferId);
            }
            _soundIdentifier.Clear();
            foreach (int slot in _soundChannels)
            {
                int target = _soundChannels[slot];
                Al.alDeleteSources(1, ref target);
            }
            Alut.alutExit();
        }

        #endregion
    }
}
