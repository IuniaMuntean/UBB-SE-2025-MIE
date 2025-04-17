using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App1.Iunia_Fabi.Model
{
    [Table("roads")]
    internal class Road
    {
        [Column("startcity")]
        public int start { get; set; }

        [Column("endcity")]
        public int end { get; set; }

        [Column("value")]
        public float value { get; set; }

        //public int RelatedEntity1 { get; set; }
        //public int RelatedEntity2 { get; set; }

        public Road(int start, int end, float value)
        {
            this.start = start;
            this.end = end;
            this.value = value;
        }
    }
}
