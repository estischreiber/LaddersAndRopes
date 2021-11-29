using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaddersAndSnake
{
    public class Tile
    {
        private int startTileNum;

        private int endTileNum;

        private string tileType;

        public int StartTileNum { get { return startTileNum; } set { startTileNum = value; } }
        public int EndTileNum { get { return endTileNum; } set { endTileNum = value; } }
        public string TileType { get { return tileType; } set { tileType = value; } }
    }
}
