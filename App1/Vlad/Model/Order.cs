using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace App1.Vlad.Model
{
    [Table("orders")]
    public class Order
    {
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("client_name")]
        public string ClientName { get; set; }
        [Column("cargo_type")]
        public string CargoType { get; set; }
        [Column("cargo_weight")]
        public double CargoWeight { get; set; }
        [Column("source_city")]
        public string SourceCity { get; set; }
        [Column("destination_city")]
        public string DestinationCity { get; set; }
    }
}
