using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter4_7
{
    class World
    {
        bool _playerHasWonGame = false;
        public void Update()
        {
            Entity player = FindEntity("Player");

            UpdateGameCreatures();
            UpdatePlayer(player);
            UpdateEnvironment();
            UpdateEffects();

            // Extracted function
            CheckForGameOver(player);

        }

        private void CheckForGameOver(Entity player)
        {
            Entity goldenEagle = FindEntity("GoldenEagle");

            if (player.Inventory.Contains(goldenEagle))
            {
                _playerHasWonGame = true;
                ChangeGameState("PlayerWinState");
            }
        }


        // Example functions
        private void ChangeGameState(string stateName)
        {
            throw new NotImplementedException();
        }

        private Entity FindEntity(string entityName)
        {
            throw new NotImplementedException();
        }

        private void UpdateEffects()
        {
            throw new NotImplementedException();
        }

        private void UpdateEnvironment()
        {
            throw new NotImplementedException();
        }

        private void UpdatePlayer(Entity player)
        {
            throw new NotImplementedException();
        }

        private void UpdateGameCreatures()
        {
            throw new NotImplementedException();
        }

        // additional code
    }

}
