using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Engine;

namespace Chapter10_10
{
    public class BulletManager
    {
        List<Bullet> _bullets = new List<Bullet>();
        RectangleF _bounds;

        public BulletManager(RectangleF playArea)
        {
            _bounds = playArea;
        }

        public void Shoot(Bullet bullet)
        {
            _bullets.Add(bullet);
        }

        public void Update(double elapsedTime)
        {
            _bullets.ForEach(x => x.Update(elapsedTime));
            CheckOutOfBounds();
            RemoveDeadBullets();
        }

        private void CheckOutOfBounds()
        {
            foreach (Bullet bullet in _bullets)
            {
                if (!bullet.GetBoundingBox().IntersectsWith(_bounds))
                {
                    bullet.Dead = true;
                }
            }
        }

        private void RemoveDeadBullets()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                if (_bullets[i].Dead)
                {
                    _bullets.RemoveAt(i);
                }
            }
        }

        internal void Render(Renderer renderer)
        {
            _bullets.ForEach(x => x.Render(renderer));
        }

        internal void UpdateEnemyCollisions(Enemy enemy)
        {
            foreach (Bullet bullet in _bullets)
            {
                if (bullet.GetBoundingBox().IntersectsWith(enemy.GetBoundingBox()))
                {
                    bullet.Dead = true;
                    enemy.OnCollision(bullet);
                }
            }

        }
    }

}
