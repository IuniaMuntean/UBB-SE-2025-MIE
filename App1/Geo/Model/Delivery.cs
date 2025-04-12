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
        [Column("Truck")]
        public string Truck { get; set; }
        [Column("Departure_Time")]
        public string GoTime { get; set; }
        [Column("Arrival_Time")]
        public string ArrTime { get; set; }
        [Column("Cargo_Weight")]
        public int Weight { get; set; }
        [Column("Cargo_Type")]
        public string CargoType {  get; set; }

        [NotMapped]
        public string WeightDisplay => Weight <= 44000 ? $"{Weight} kg" : "⚠️ Exceeds regulations (44000 kg)";

        [NotMapped]
        public string TimeDisplay => $"Departure: {GoTime}\nArrival: {ArrTime}";

        [NotMapped]
        public string StaffDisplay => $"Manager: {Manager}\nDriver: {Driver}";

        [NotMapped]
        public string RouteDisplay => $"From: {Departure}\nTo: {Destination}";

        [NotMapped]
        public string CargoDisplay => $"Type: {CargoType}\nWeight: {WeightDisplay}";

        [NotMapped]
        public string StatusDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(Driver))
                    return "No driver assigned";
                if (Weight > 44000)
                    return "Weight exceeds regulations";
                if (string.IsNullOrEmpty(GoTime))
                    return "Departure time not set";
                if (string.IsNullOrEmpty(ArrTime))
                    return "In transit";
                return "Scheduled";
            }
        }
    }
}
