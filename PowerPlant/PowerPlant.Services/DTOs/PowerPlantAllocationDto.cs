using System.Text.Json.Serialization;

namespace PowerPlant.Services.DTOs
{
    public class PowerPlantAllocationDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("p")]
        public double P { get; set; }
    }
}
