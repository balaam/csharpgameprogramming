using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Tao.OpenGl;

namespace Chapter10_4
{
    class InnerGameState : IGameObject
    {
        Renderer _renderer = new Renderer();
        Input _input;
        StateSystem _system;
        PersistantGameData _gameData;
        Font _generalFont;

        double _gameTime;

        public InnerGameState(StateSystem system, Input input, PersistantGameData gameData, Font generalFont)
        {
            _input = input;
            _system = system;
            _gameData = gameData;
            _generalFont = generalFont;
            OnGameStart();
        }

        public void OnGameStart()
        {
            _gameTime = _gameData.CurrentLevel.Time;
        }

        #region IGameObject Members

        public void Update(double elapsedTime)
        {
            _gameTime -= elapsedTime;

            if (_gameTime <= 0)
            {
                OnGameStart();
                _gameData.JustWon = true;
                _system.ChangeState("game_over");
            }
        }

        public void Render()
        {
            Gl.glClearColor(1, 0, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.Render();
        }

        #endregion
    }

}
