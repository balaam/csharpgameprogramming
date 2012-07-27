using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter10_11
{
    class EnemyManager
    {
        List<Enemy> _enemies = new List<Enemy>();
        TextureManager _textureManager;
        EffectsManager _effectsManager;
        int _leftBound;

        public List<Enemy> EnemyList
        {
            get
            {
                return _enemies;
            }
        }

        public EnemyManager(TextureManager textureManager, EffectsManager effectsManager, int leftBound)
        {
            _textureManager = textureManager;
            _effectsManager = effectsManager;
            _leftBound = leftBound;

            // Add a test enemy.
            Enemy enemy = new Enemy(_textureManager, _effectsManager);
            _enemies.Add(enemy);
        }

        public void Update(double elapsedTime)
        {
            _enemies.ForEach(x => x.Update(elapsedTime));
            CheckForOutOfBounds();
            RemoveDeadEnemies();
        }

        private void CheckForOutOfBounds()
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy.GetBoundingBox().Right < _leftBound)
                {
                    enemy.Health = 0; // kill the enemy off
                }
            }
        }

        public void Render(Renderer renderer)
        {
            _enemies.ForEach(x => x.Render(renderer));
        }

        private void RemoveDeadEnemies()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (_enemies[i].IsDead)
                {
                    _enemies.RemoveAt(i);
                }
            }
        }
    }

}
