using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using App1.Vlad.Model;

namespace App1.Geo.Model
{
    [Table("delivery")]
    public class Delivery
    {
        [Column("delivery_id")]
        public int Id { get; set; }
        [Column("manager")]
        public string Manager { get; set; } = "Default Manager";
        [Column("departure")]
        public string Departure { get; set; }
        [Column("destination")]
        public string Destination { get; set; }
        [Column("distance")]
        public decimal Distance { get; set; }
        [Column("driver")]
        public string Driver { get; set; }
        [Column("departure_time")]
        public DateTime DepartureTime { get; set; }
        [Column("arrival_time")]
        public DateTime ArrivalTime { get; set; }
        [Column("truck_id")]
        public int TruckId { get; set; }
        [Column("cargo_weight")]
        public decimal Weight { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
