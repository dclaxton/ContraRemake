﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NotContra
{
    class Terrain : IViewable
    {
        private int maxX;
        private int maxY;
        private bool[,] ledgeMap;


        public Terrain()
        {
            Tiles = new List<Tile>();
            maxX = 0;
            maxY = 0;
            ledgeMap = null;

        }

        public void BuildLedgeMap()
        {
            ledgeMap = new bool[maxX + 1, maxY + 1];

            foreach(Tile tile in Tiles)
            {
                int x = tile.X / ImageSelector.IMAGE_WIDTH;
                int y = tile.Y / ImageSelector.IMAGE_HEIGHT;
                ledgeMap[x,y] = tile.Code == TileCode.LEDGE;
            }
        }

        public bool IsLedgeAt(int x, int y)
        {
            x /= ImageSelector.IMAGE_WIDTH;
            y /= ImageSelector.IMAGE_HEIGHT;

            if(x < 0 || y < 0 || x > maxX || y > maxY)
            {
                return true;
            }

            return ledgeMap[x, y];
        }

        public void Add(Tile tile)
        {
            CheckForNullTile(tile);
            Tiles.Add(tile);

            if(tile.X > maxX)
            {
                maxX = tile.X;
            }

            if(tile.Y > maxY)
            {
                maxY = tile.Y;
            }
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
