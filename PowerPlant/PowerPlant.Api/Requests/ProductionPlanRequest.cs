using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PowerPlant.Api.Requests
{
    public class ProductionPlanRequest
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Load must be positive")]
        public double Load { get; set; }

        [Required]
        public FuelsRequest Fuels { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "PowerPlants cannot be empty")]
        public List<PowerPlantRequest> PowerPlants { get; set; }
    }

    public class FuelsRequest
    {
        [Required]
        [JsonPropertyName("gas(euro/MWh)")]
        [Range(0, double.MaxValue, ErrorMessage = "gas(euro/MWh) must be positive")]
        public double Gas { get; set; }

        [Required]
        [JsonPropertyName("kerosine(euro/MWh)")]
        [Range(0, double.MaxValue, ErrorMessage = "kerosine(euro/MWh) must be positive")]
        public double Kerosine { get; set; }

        [Required]
        [JsonPropertyName("co2(euro/ton)")]
        [Range(0, double.MaxValue, ErrorMessage = "co2(euro/ton) must be positive")]
        public double Co2 { get; set; }

        [Required]
        [JsonPropertyName("wind(%)")]
        [Range(0, 100, ErrorMessage = "wind(%) must be between 0 and 100")]
        public double Wind { get; set; }
    }

    public class PowerPlantRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PowerPlantType Type { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Efficiency must be between 0 and 1")]
        public double Efficiency { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Pmin must be positive")]
        public double Pmin { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Pmax must be positive")]
        public double Pmax { get; set; }
    }

    public enum PowerPlantType
    {
        GasFired,
        TurboJet,
        WindTurbine
    }
}
