using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App1.Model
{
    internal class City
    {
        [Table("cities")]
        internal class City
        {
            [Column("id")]
            public int id { get; set; }

            [Column("name")]
            public string name { get; set; }

            [Column("x")]
            public int x;

            [Column("y")]
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
