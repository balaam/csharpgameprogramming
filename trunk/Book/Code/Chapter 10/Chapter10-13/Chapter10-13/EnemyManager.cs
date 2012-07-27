using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Chapter10_13
{

    class EnemyManager
    {
        List<Enemy> _enemies = new List<Enemy>();
        TextureManager _textureManager;
        EffectsManager _effectsManager;
        int _leftBound;
        List<EnemyDef> _upComingEnemies = new List<EnemyDef>();

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

            _textureManager = textureManager;
            _effectsManager = effectsManager;
            _leftBound = leftBound;

            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 30));
            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 29.5));
            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 29));
            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 28.5));

            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 25));
            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 24.5));
            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 24));
            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 23.5));

            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 20));
            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 19.5));
            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 19));
            _upComingEnemies.Add(new EnemyDef("cannon_fodder", 18.5));


            // Sort enemies so the greater launch time appears first.
            _upComingEnemies.Sort(delegate(EnemyDef firstEnemy, EnemyDef secondEnemy)
            {
                return firstEnemy.LaunchTime.CompareTo(secondEnemy.LaunchTime);

            });

        }

        private void UpdateEnemySpawns(double gameTime)
        {
            // If no upcoming enemies then there's nothing to spawn.
            if (_upComingEnemies.Count == 0)
            {
                return;
            }

            EnemyDef lastElement = _upComingEnemies[_upComingEnemies.Count - 1];
            if (gameTime < lastElement.LaunchTime)
            {
                _upComingEnemies.RemoveAt(_upComingEnemies.Count - 1);
                _enemies.Add(CreateEnemyFromDef(lastElement));
            }
        }

        private Enemy CreateEnemyFromDef(EnemyDef definition)
        {
            Enemy enemy = new Enemy(_textureManager, _effectsManager);
            //enemy.SetPosition(definition.StartPosition); <- this line can be removed
            if (definition.EnemyType == "cannon_fodder")
            {
                List<Vector> _pathPoints = new List<Vector>();
                _pathPoints.Add(new Vector(1400, 0, 0));
                _pathPoints.Add(new Vector(0, 250, 0));
                _pathPoints.Add(new Vector(-1400, 0, 0));


                enemy.Path = new Path(_pathPoints, 10);
            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "Unknown enemy type.");
            }

            return enemy;
        }



        public void Update(double elapsedTime, double gameTime)
        {
            UpdateEnemySpawns(gameTime);
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
