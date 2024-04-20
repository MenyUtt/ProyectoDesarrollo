using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoDesarrollo.Models
{
    public class Inventories
    {
        [Key]
        [JsonPropertyName("PRODUCT_ID")]
        public int? PRODUCT_ID { get; set; }
        [JsonPropertyName("[warehouse_id]")]
        public int? WAREHOUSE_ID { get; set; }
        [JsonPropertyName("quantity")]
        public int? QUANTITY { get; set; }
        public string? ProductName { get; set; }
        public string? WarehouseName { get; set; }


        public Warehouses warehouses { get; set; }

        public Products products { get; set; }
    }
}
