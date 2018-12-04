using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class Hero 
    {
        private readonly int TimeBetweenShots;

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
            this.Direction = 1; //1 is Right, -1 is Left
            this.HeroRemainsOnScreen = 100;
            this.TimeBetweenShots = 15;
            this.TimeTillNextShot = 0;
            this.Projectiles = new ProjectileManager();
        }

        public string Image { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int MovementX { get; private set; }
        public int MovementY { get; private set; }
        public bool IsJumping { get; private set; }
        public int JumpSpeed { get; private set; }
        public int Direction { get; private set; }
        public bool IsDead { get; private set; }
        public int HeroRemainsOnScreen { get; private set; }
        public int TimeTillNextShot { get; private set; }
        public ProjectileManager Projectiles { get; private set; }

        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>
            {
                new Tile(TileCode.PLAYER, X,Y,Image)
            };
            tiles.AddRange(Projectiles.GetTiles());
            return tiles;
        }

        internal void Dies()
        {
            this.IsDead = true;
            MovementX = 0;
            if(Direction < 0)
            {
                Image = "hero_dead_left";
            }
            else
            {
                Image = "hero_dead";
            }
        }

        internal void Left()
        {
            if(!IsDead)
            {
                Direction = -1;
                MovementX = -5;
                Image = "hero_run_left";
            }
        }

        internal void Right()
        {
            if(!IsDead)
            {
                Direction = 1;
                MovementX = 5;
                Image = "hero_run_right";
            }
        }

        internal void Shoot()
        {
            if (!IsDead && TimeTillNextShot == 0)
            {
                Projectiles.Add(new Projectile(this.X, this.Y, this.Direction));
                TimeTillNextShot = TimeBetweenShots;

                if(Direction < 0)
                {
                    Image = "hero_shoot_left";
                }
                else
                {
                    Image = "hero_shoot_right";
                }
            }
        }

        internal void Update(Terrain terrain)
        {
            Projectiles.UpdateProjectiles();

            if(TimeTillNextShot > 0)
            {
                TimeTillNextShot--;
            }

            if(IsDead)
            {
                HeroRemainsOnScreen--;
            }

            X += MovementX;
            Y += MovementY;

            if(Y > 800)
            {
                IsDead = true;
            }

            if (IsJumping && MovementY <= JumpSpeed)
            {
                MovementY += 1;
            }

            if (!IsJumping && !terrain.IsLedgeAt(X, Y + ImageSelector.IMAGE_HEIGHT) &&
                !terrain.IsLedgeAt(X + ImageSelector.IMAGE_WIDTH / 2, Y + ImageSelector.IMAGE_HEIGHT) &&
                !terrain.IsLedgeAt(X + ImageSelector.IMAGE_WIDTH, Y + ImageSelector.IMAGE_HEIGHT))
            {
                IsJumping = true;
            }

            if (MovementY >= 0 && terrain.IsLedgeAt(X, Y + ImageSelector.IMAGE_HEIGHT))
            {
                IsJumping = false;
                MovementY = 0;
            }

            if(MovementY == 0 && terrain.IsLedgeAt(X, Y + ImageSelector.IMAGE_HEIGHT -1))
            {
                Y = (int)(Y / ImageSelector.IMAGE_HEIGHT) * ImageSelector.IMAGE_HEIGHT;
            }
        }

        internal void StopRunning()
        {
            if (!IsDead && !IsJumping)
            {
                MovementX = 0;
                if (Image.Equals("hero_run_right"))
                {
                    Image = "hero_idle_right";
                }

                if (Image.Equals("hero_run_left"))
                {
                    Image = "hero_idle_left";
                }
            }
        }

        internal void Jump()
        {
            if(!IsJumping && !IsDead)
            {
                MovementY = -JumpSpeed;
                IsJumping = true;
            }

        }
    }
}
