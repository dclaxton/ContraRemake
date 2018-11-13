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
            this.MovementX = 0;
            this.MovementY = 0;
            this.Image = "hero_idle_right";
            this.IsJumping = false;
            this.JumpSpeed = 20;
            this.Projectiles = new List<Projectile>();
            this.Direction = 1; //1 is Right, -1 is Left
        }

        public string Image { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int MovementX { get; private set; }
        public int MovementY { get; private set; }
        public bool IsJumping { get; private set; }
        public int JumpSpeed { get; private set; }
        public List<Projectile> Projectiles { get; private set; }
        public int Direction { get; private set; }

        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>
            {
                new Tile(TileCode.PLAYER, X,Y,Image)
            };
            foreach (var projectile in Projectiles)
            {
                tiles.AddRange(projectile.GetTiles());
            }
            return tiles;
        }

        internal void Left()
        {
            Direction = -1;
            MovementX = -5;
            Image = "hero_run_left";
        }

        internal void Right()
        {
            Direction = 1;
            MovementX = 5;
            Image = "hero_run_right";
        }

        internal void Shoot()
        {
            this.Projectiles.Add(new Projectile(this.X, this.Y, this.Direction));
        }

        internal void Update(Terrain terrain)
        {
            X += MovementX;
            Y += MovementY;

            if(IsJumping && MovementY <= JumpSpeed)
            {
                MovementY += 1;
            }

            if (!IsJumping && !terrain.IsLedgeAt(X, Y + ImageSelector.IMAGE_HEIGHT))
            {
                IsJumping = true;
            }

            if(MovementY >= 0 && terrain.IsLedgeAt(X,Y + ImageSelector.IMAGE_HEIGHT))
            {
                IsJumping = false;
                MovementY = 0;
            }

            foreach (var projectile in Projectiles)
            {
                projectile.Update();
            }
        }

        internal void StopRunning()
        {
            MovementX = 0;
            if(Image.Equals("hero_run_right"))
            {
                Image = "hero_idle_right";
            }

            if(Image.Equals("hero_run_left"))
            {
                Image = "hero_idle_left";
            }
        }

        internal void Jump()
        {
            if(!IsJumping)
            {
                MovementY = -JumpSpeed;
                IsJumping = true;
            }

        }
    }
}
