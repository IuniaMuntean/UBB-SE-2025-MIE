using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace App1.Geo.Model
{
    [Table("Delivery")]
    public class Delivery
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Manager")]
        public string Manager{ get; set; }
        [Column("Departure")]
        public string Departure { get; set; }
        [Column("Destination")]
        public string Destination {  get; set; }
        [Column("Client")]
        public string Client { get; set; }
        [Column("Driver")]
        public string Driver {  get; set; }
        [Column("Departure_Time")]
        public string GoTime { get; set; }
        [Column("Arrival_Time")]
        public string ArrTime { get; set; }
        [Column("Cargo_Weight")]
        public int Weight { get; set; }
        [Column("Cargo_Type")]
        public string CargoType {  get; set; }

    }
}
