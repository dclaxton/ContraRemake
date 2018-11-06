using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class Terrain
    {
        public Terrain()
        {
            Tiles = new List<Tile>();

        }

        public void Add(Tile tile)
        {
            CheckForNullTile(tile);
            Tiles.Add(tile);
        }

        private static void CheckForNullTile(Tile tile)
        {
            if (tile.Equals(null))
            {
                throw new ArgumentNullException("Tile is null");
            }
        }

        public List<Tile> Tiles { get; private set; }
    }
}
