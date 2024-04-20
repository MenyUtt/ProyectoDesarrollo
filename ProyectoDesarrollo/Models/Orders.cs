using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoDesarrollo.Models
{
    public class Orders
    {
        [Key]
        public int? ORDER_ID { get; set; }
        [JsonPropertyName("contact_id")]
        public int? CUSTOMER_ID { get; set; }
        [JsonPropertyName("customer_id")]
        public string? STATUS { get; set; }
        [JsonPropertyName("status")]
        public int? SALESMAN_ID { get; set; }
        [JsonPropertyName("salesman_id")]

        public DateTime? ORDER_DATE { get; set; }

        public Customers Customer { get; set; }
    }
}
