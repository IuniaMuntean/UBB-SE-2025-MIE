using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Model
{
    internal class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public int x;
        public int y;

        public City(int id, string name, int x, int y)
        {
            this.id = id;
            this.name = name;
            this.x = x;
            this.y = y;
        }
        public City(City other)
        {
            this.id = other.id;
            this.name = other.name;
            this.x = other.x;
            this.y = other.y;
        }
    }
}
