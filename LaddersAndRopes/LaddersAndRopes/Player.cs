using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaddersAndSnake
{
    public class Player
    {
        private string name;
        private int location;

        public string Name { get { return name; } set { name = value; } }
        public int Location { get { return location; } set { location = value; } }
    }
}
