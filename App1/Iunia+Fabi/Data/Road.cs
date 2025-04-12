using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App1.Model
{
    [Table("roads")]
    internal class Road
    {
        [Column("startcity")]
        public int start { get; set; }

        [Column("endcity")]
        public int end { get; set; }

        [Column("value")]
        public int value { get; set; }

        public Road(int start, int end, int value)
        {
            this.start = start;
            this.end = end;
            this.value = value;
        }
    }
}
