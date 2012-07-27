using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using System.Windows.Forms;

namespace Chapter10_6
{
    class Level
    {
        Input _input;
        PersistantGameData _gameData;
        PlayerCharacter _playerCharacter;
        TextureManager _textureManager;

        ScrollingBackground _background;
        ScrollingBackground _backgroundLayer;

        public Level(Input input, TextureManager textureManager, PersistantGameData gameData)
        {
            _input = input;
            _gameData = gameData;
            _textureManager = textureManager;

            _background = new ScrollingBackground(textureManager.Get("background"));
            _background.SetScale(2, 2);
            _background.Speed = 0.15f;

            _backgroundLayer = new ScrollingBackground(textureManager.Get("background_layer_1"));
            _backgroundLayer.Speed = 0.1f;
            _backgroundLayer.SetScale(2.0, 2.0);


            _playerCharacter = new PlayerCharacter(_textureManager);
        }

        public void Update(double elapsedTime)
        {
            _background.Update((float)elapsedTime);
            _backgroundLayer.Update((float)elapsedTime);

            // Get controls and apply to player character
            double _x = _input.Controller.LeftControlStick.X;
            double _y = _input.Controller.LeftControlStick.Y * -1;
            Vector controlInput = new Vector(_x, _y, 0);

            if (Math.Abs(controlInput.Length()) < 0.0001)
            {
                // If the input is very small, then the player may not be using 
                // a controller;, they might be using the keyboard.
                if (_input.Keyboard.IsKeyHeld(Keys.Left))
                {
                    controlInput.X = -1;
                }

                if (_input.Keyboard.IsKeyHeld(Keys.Right))
                {
                    controlInput.X = 1;
                }

                if (_input.Keyboard.IsKeyHeld(Keys.Up))
                {
                    controlInput.Y = 1;
                }

                if (_input.Keyboard.IsKeyHeld(Keys.Down))
                {
                    controlInput.Y = -1;
                }
            }

            _playerCharacter.Move(controlInput * elapsedTime);

        }

        public void Render(Renderer renderer)
        {
            _background.Render(renderer);
            _backgroundLayer.Render(renderer);
            _playerCharacter.Render(renderer);
        }
    }
}
