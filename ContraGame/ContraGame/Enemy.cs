using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    abstract class Enemy
    {
        public Enemy(int hitpoints, int x, int y, int movementX, int movementY)
        {
            this.HitPoints = hitpoints;
            this.X = x;
            this.Y = y;
            this.MovementX = movementX;
            this.MovementY = movementY;
            this.IsJumping = false;

        }

        public void TakeDamage(int damage)
        {
            if(damage < 1)
            {
                throw new ArgumentException("Damage must be positive!");
            }
            HitPoints -= damage;
        }

        public bool IsDead() => HitPoints <= 0;

        public void Update(Terrain terrain)
        {
            X += MovementX;
            Y += MovementY;

            if(IsJumping)
            {
                MovementY += 1;
            }

            if(!IsJumping && !terrain.IsLedgeAt(X,Y+ImageSelector.IMAGE_HEIGHT))
            {
                IsJumping = true;
            }

            if(terrain.IsLedgeAt(X,Y+ImageSelector.IMAGE_HEIGHT))
            {
                IsJumping = false;
                MovementY = 0;
            }
        }

        public int HitPoints { get; private set; }
        public bool IsJumping { get; private set; }
        public int MovementX { get; set; }
        public int MovementY { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }
    }
}
