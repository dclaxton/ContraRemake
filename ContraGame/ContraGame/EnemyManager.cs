using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class EnemyManager : IViewable
    {
        private readonly Random rand;

        public EnemyManager()
        {
            this.Enemies = new List<ZombieLad>();
            this.rand = new Random();
        }

        public void GenerateEnemy(Hero hero)
        {
            int rollToGenerate = rand.Next(80);

            if(rollToGenerate == 0)
            {
                int x = hero.X + 600;
                int y = hero.Y;

                ZombieLad lad = new ZombieLad(x, y, -2);
                this.Enemies.Add(lad);
            }
        }

        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>();
            foreach(var enemy in Enemies)
            {
                tiles.AddRange(enemy.GetTiles());
            }
            return tiles;
        }

        public List<ZombieLad> Enemies { get; private set; }

        internal void UpdateEnemies(Terrain terrain)
        {
            foreach(ZombieLad lad in Enemies)
            {
                lad.Update(terrain);
            }
        }
    }
}
