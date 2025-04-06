using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Model
{
    internal class Road
    {
        public int start { get; set; }
        public int end { get; set; }
        public int value { get; set; }

        public Road(int start, int end, int value)
        {
            this.start = start;
            this.end = end;
            this.value = value;
        }
    }
}
