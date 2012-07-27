using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Engine;

namespace Chapter10_16
{
    public class BulletManager
    {
        List<Bullet> _bullets = new List<Bullet>();
        List<Bullet> _enemyBullets = new List<Bullet>();

        RectangleF _bounds;

        public BulletManager(RectangleF playArea)
        {
            _bounds = playArea;
        }

        public void Shoot(Bullet bullet)
        {
            _bullets.Add(bullet);
        }

        public void EnemyShoot(Bullet bullet)
        {
            _enemyBullets.Add(bullet);
        }

        public void UpdatePlayerCollision(PlayerCharacter playerCharacter)
        {
            foreach (Bullet bullet in _enemyBullets)
            {
                if (bullet.GetBoundingBox().IntersectsWith(playerCharacter.GetBoundingBox()))
                {
                    bullet.Dead = true;
                    playerCharacter.OnCollision(bullet);
                }
            }
        }


        public void Update(double elapsedTime)
        {
            UpdateBulletList(_bullets, elapsedTime);
            UpdateBulletList(_enemyBullets, elapsedTime);
        }

        public void UpdateBulletList(List<Bullet> bulletList, double elapsedTime)
        {
            bulletList.ForEach(x => x.Update(elapsedTime));
            CheckOutOfBounds(_bullets);
            RemoveDeadBullets(bulletList);
        }

        private void CheckOutOfBounds(List<Bullet> bulletList)
        {
            foreach (Bullet bullet in bulletList)
            {
                if (!bullet.GetBoundingBox().IntersectsWith(_bounds))
                {
                    bullet.Dead = true;
                }
            }
        }

        private void RemoveDeadBullets(List<Bullet> bulletList)
        {
            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                if (bulletList[i].Dead)
                {
                    bulletList.RemoveAt(i);
                }
            }
        }

        internal void Render(Renderer renderer)
        {
            _bullets.ForEach(x => x.Render(renderer));
            _enemyBullets.ForEach(x => x.Render(renderer));
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
