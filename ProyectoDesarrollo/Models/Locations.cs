using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoDesarrollo.Models
{
    public class Locations
    {
        [Key]
        [JsonPropertyName("location_id")]
        public int LOCATION_ID { get; set; }
        [JsonPropertyName("address")]
        public string? ADDRESS { get; set; }
        [JsonPropertyName("postal_code")]
        public string? POSTAL_CODE { get; set; }
        [JsonPropertyName("city")]
        public string? CITY { get; set; }
        [JsonPropertyName("state")]
        public string? STATE { get; set; }

        [JsonPropertyName("country_id")]
        public string? COUNTRY_ID { get; set; }

    }
}
