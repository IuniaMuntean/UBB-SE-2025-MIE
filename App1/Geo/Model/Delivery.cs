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
        public String Manager{ get; set; }
        [Column("Departure")]
        public String Departure { get; set; }
        [Column("Destination")]
        public String Destination {  get; set; }
        [Column("Distance")]
        public int Distance {  get; set; }
        [Column("Driver")]
        public String Driver {  get; set; }
        [Column("Departure_Time")]
        public String GoTime { get; set; }
        [Column("Arrival_Time")]
        public String ArrTime { get; set; }
        [Column("TruckId")]
        public int Truck {  get; set; }
        [Column("Cargo_Weight")]
        public int Weight { get; set; }
        [Column("Cargo_IDs")]
        public string CargoIds { get; set; }  

    }
}
