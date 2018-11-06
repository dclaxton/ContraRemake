using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class Hero : IViewable
    {
        public Hero(Tile start)
        {
            if(start.Equals(null))
            {
                throw new ArgumentNullException("Start tile cannot be null");
            }

            this.X = start.X;
            this.Y = start.Y;
            this.Image = "hero_idle_right";
            this.IsJumping = false;
            this.JumpSpeed = 0;
        }

        public string Image { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsJumping { get; private set; }
        public int JumpSpeed { get; private set; }

        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>
            {
                new Tile(TileCode.PLAYER, X,Y,Image)
            };

        return tiles;
        }

        internal void Left(Terrain terrain)
        {
            X -= 5;
            Image = "hero_run_left";
        }

        internal void Right(Terrain terrain)
        {
            X += 5;
            Image = "hero_run_right";
        }

        internal void StopRunning()
        {
            if(Image.Equals("hero_run_right"))
            {
                Image = "hero_idle_right";
            }

            if(Image.Equals("hero_run_left"))
            {
                Image = "hero_idle_left";
            }
        }

        internal void Jump(Terrain terrain)
        {
            if(!IsJumping)
            {
                JumpSpeed = 10;
                IsJumping = true;
            }

        }
    }
}
