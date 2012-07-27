using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter7_5
{
    class StateSystem
    {
        Dictionary<string, IGameObject> _stateStore = new Dictionary<string, IGameObject>();
        IGameObject _currentState = null;
        public void Update(double elapsedTime)
        {
            if (_currentState == null)
            {
                return; // nothing to process
            }
            _currentState.Update(elapsedTime);
        }

        public void Render()
        {
            if (_currentState == null)
            {
                return; // nothing to render
            }
            _currentState.Render();
        }

        public void AddState(string stateId, IGameObject state)
        {
            System.Diagnostics.Debug.Assert(Exists(stateId) == false);
            _stateStore.Add(stateId, state);
        }

        public void ChangeState(string stateId)
        {
            System.Diagnostics.Debug.Assert(Exists(stateId));
            _currentState = _stateStore[stateId];
        }

        /// <summary>
        /// Check if a state exists.
        /// </summary>
        /// <param name="stateId">The id of the state to check.</param>
        /// <returns>True for an existant state otherwise false</returns>
        public bool Exists(string stateId)
        {
            return _stateStore.ContainsKey(stateId);
        }
    }
}
