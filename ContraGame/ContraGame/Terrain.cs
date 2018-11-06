using System;
using System.Collections.Generic;
using System.Linq;

namespace NotContra
{
    class Terrain : IViewable
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

        public Tile GetStart()
        {
            var start_tiles = from tile in Tiles
                              where tile.Code.Equals(TileCode.START)
                              select tile;
            if(!start_tiles.Count().Equals(1) )
            {
                throw new InvalidOperationException("There are no start tiles in this terrain!");
            }

            return start_tiles.First();
        }

        public List<Tile> Tiles { get; private set; }

        public List<Tile> GetTiles()
        {
            return Tiles;
        }
    }
}
