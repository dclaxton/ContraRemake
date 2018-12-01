using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class ProjectileManager : IViewable
    {

        public ProjectileManager()
        {
            this.Projectiles = new List<Projectile>();
        }

        public void Add(Projectile p)
        {
            Projectiles.Add(p);
        }

        public void UpdateProjectiles()
        {
            this.Projectiles = this.Projectiles.FindAll( projectile => projectile.TimeToLive > 0);

            foreach (Projectile projectile in Projectiles)
            {
                projectile.Update();
            }
        }

        internal void ShootEnemies(List<Enemy> enemies)
        {
            foreach (Projectile projectile in Projectiles)
            {
                int x = projectile.X + ImageSelector.IMAGE_WIDTH / 2;
                int y = projectile.Y + ImageSelector.IMAGE_HEIGHT / 2;

                foreach (Enemy enemy in enemies)
                {
                    if (x > enemy.X &&
                        x < enemy.X + ImageSelector.IMAGE_WIDTH &&
                        y > enemy.Y &&
                        y < enemy.Y + ImageSelector.IMAGE_HEIGHT &&
                        !enemy.IsDead())
                    {
                        enemy.TakeDamage(1);
                        projectile.Dissolve();
                    }
                }
            }
        }

        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Projectile projectile in Projectiles)
            {
                tiles.AddRange(projectile.GetTiles());
            }
            return tiles;
        }

        public List<Projectile> Projectiles { get; private set; }
    }
}
