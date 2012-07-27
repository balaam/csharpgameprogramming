using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using System.Windows.Forms;
using System.Drawing;

namespace Chapter10_12
{
    class Level
    {
        BulletManager _bulletManager = new BulletManager(new RectangleF(-1300 / 2, -750 / 2, 1300, 750));
        Input _input;
        PersistantGameData _gameData;
        PlayerCharacter _playerCharacter;
        TextureManager _textureManager;
        EffectsManager _effectsManager;

        ScrollingBackground _background;
        ScrollingBackground _backgroundLayer;

        // List<Enemy> _enemyList = new List<Enemy>(); <- Removed
        EnemyManager _enemyManager;

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

            _playerCharacter = new PlayerCharacter(_textureManager, _bulletManager);

            _effectsManager = new EffectsManager(_textureManager);
            //  _enemyList.Add(new Enemy(_textureManager, _effectsManager)); <- Removed

            // -1300 is bad for two reasons
            // 1. It's a magic number in the middle of the code
            // 2. It's based on the size of the form but doesn't directly reference the size of the form
            // this means duplication and two places to edit the code if the form size changes.
            // The form size and the enemy manager play area size should both get their value
            // from one central place.
            _enemyManager = new EnemyManager(_textureManager, _effectsManager, -1300);
        }

        private void UpdateCollisions()
        {
            foreach (Enemy enemy in _enemyManager.EnemyList)
            {
                if (enemy.GetBoundingBox().IntersectsWith(_playerCharacter.GetBoundingBox()))
                {
                    enemy.OnCollision(_playerCharacter);
                    _playerCharacter.OnCollision(enemy);
                }

                _bulletManager.UpdateEnemyCollisions(enemy);

            }

        }


        private void UpdateInput(double elapsedTime)
        {
            if (_input.Keyboard.IsKeyPressed(Keys.Space) || _input.Controller.ButtonA.Pressed)
            {
                _playerCharacter.Fire();
            }
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



        public void Update(double elapsedTime, double gameTime)
        {
            _effectsManager.Update(elapsedTime);
            _playerCharacter.Update(elapsedTime);

            _background.Update((float)elapsedTime);
            _backgroundLayer.Update((float)elapsedTime);

            UpdateCollisions();
            //_enemyList.ForEach(x => x.Update(elapsedTime));
            _enemyManager.Update(elapsedTime, gameTime);
            _bulletManager.Update(elapsedTime);

            UpdateInput(elapsedTime);
        }

        public void Render(Renderer renderer)
        {
            _background.Render(renderer);
            _backgroundLayer.Render(renderer);

            // _enemyList.ForEach(x => x.Render(renderer));
            _enemyManager.Render(renderer);
            _playerCharacter.Render(renderer);
            _bulletManager.Render(renderer);
            renderer.Render();
            _effectsManager.Render(renderer);
            renderer.Render();
        }

        internal bool HasPlayerDied()
        {
            return _playerCharacter.IsDead;
        }
    }
}
