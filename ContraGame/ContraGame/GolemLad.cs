using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class GolemLad : Enemy,IViewable
    {
        public GolemLad(int x, int y, int movementX) :
            base(3, x, y, movementX, 0)
        {
            Image = "golem_walking";

            if (this.MovementX < 0)
            {
                Image = "golem_walking_left";
            }
        }

        public string Image { get; private set; }

        public override string GetImage()
        {
            if (IsDead() && MovementX >= 0)
            {
                Image = "golem_dead";
                MovementX = 0;
            }

            if (IsDead() && MovementX < 0)
            {
                Image = "golem_dead_left";
                MovementX = 0;
            }

            return Image;
        }
    }
}
