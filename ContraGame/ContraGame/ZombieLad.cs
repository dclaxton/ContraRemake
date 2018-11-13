using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class ZombieLad : Enemy, IViewable
    {
        public ZombieLad(int x, int y, int movementX) :
            base(1, x, y, movementX, 0)
        {
            Image = "zombie_walk";

            if (this.MovementX < 0)
            {
                Image = "zombie_walk_left";
            }
        }



        public string Image { get; private set; }

        public List<Tile> GetTiles()
        {
            if (IsDead() && MovementX >= 0)
            {
                Image = "zombie_dead";
            }
            if (IsDead() && MovementX < 0)
            {
                Image = "zombie_dead_left";
            }

            return new List<Tile>
            {
                new Tile(TileCode.ENEMY,X,Y,Image)
            };
        }
    }
}
