using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class GameModel
    {
        public GameModel(Terrain terrain)
        {
            if(terrain == null)
            {
                throw new ArgumentNullException("Terrain is null.");
            }

            this.Terrain = terrain;
        }

        public Terrain Terrain { get; private set; }

        public List<Tile> GetTiles() //def gonna change 
        {
            return Terrain.Tiles;
        }


    }
}
