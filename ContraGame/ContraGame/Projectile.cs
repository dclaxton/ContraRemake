using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class Projectile : IViewable
    {
        public Projectile(int x, int y, int direction, bool upgraded)
        {
            this.X = x;
            this.Y = y;
            this.Direction = direction;
            this.Speed = 20;
            this.TimeToLive = 100;
            this.isUpgraded = upgraded;

        }

        public void Update()
        {
            this.X += Direction * Speed;
            this.TimeToLive--;
        }

        public List<Tile> GetTiles()
        {
            if(!this.isUpgraded)
            {
                return new List<Tile>
                {
                new Tile(TileCode.PROJECTILE, X, Y, "Projectile")
                };
            }
            else
            {
                return new List<Tile>
                {
                new Tile(TileCode.PROJECTILE, X, Y, "uProjectile")
                };
            }
        }

        public int Direction { get; private set; }
        public int Speed { get; private set; }
        public int TimeToLive { get; private set; }
        public bool isUpgraded { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        internal void Dissolve()
        {
            TimeToLive = -1;
        }
    }
}
