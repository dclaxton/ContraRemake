using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class GameModel : IViewable
    {
        public GameModel(Terrain terrain, Hero hero)
        {
            if(terrain.Equals(null))
            {
                throw new ArgumentNullException("Terrain is null.");
            }

            if(hero.Equals(null))
            {
                throw new ArgumentNullException("Hero is null.");
            }

            Terrain = terrain;
            Hero = hero;
        }

        public Terrain Terrain { get; private set; }

        public Hero Hero { get; private set; }

        public List<Tile> GetTiles() //def gonna change 
        {
            List<Tile> tiles = new List<Tile>();
            tiles.AddRange(Terrain.GetTiles());
            tiles.AddRange(Hero.GetTiles());
            return tiles;
        }


    }
}
