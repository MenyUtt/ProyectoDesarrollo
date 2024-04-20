using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoDesarrollo.Models
{
    public class Contacts
    {
        [Key]
        public int? CONTACT_ID { get; set; }
        [JsonPropertyName("contact_id")]
        public string? FIRST_NAME { get; set; }
        [JsonPropertyName("first_name")]
        public string? LAST_NAME { get; set; }
        [JsonPropertyName("last_name")]
        public string? EMAIL { get; set; }
        [JsonPropertyName("email")]
        public string? PHONE { get; set; }
        [JsonPropertyName("phone")]
        public int CUSTOMER_ID { get; set; }

        public Customers Customer { get; set; }
    }
}
