using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace ProyectoDesarrollo.Models
{
    public class Customers
    {
        [Key]
        [JsonPropertyName("customer_id")]
        public int CUSTOMER_ID { get; set; }
        [JsonPropertyName("name")]
        public string? NAME { get; set; }
        [JsonPropertyName("address")]
        public string? ADDRESS { get; set; }
        [JsonPropertyName("website")]
        public string? WEBSITE { get; set; }
        [JsonPropertyName("credit_limit")]
        public decimal CREDIT_LIMIT { get; set; }
    }
}
