using System.Text.Json.Serialization;

namespace PowerPlant.Models
{
    public enum PowerPlantType
    {
        [JsonPropertyName("gasfired")]
        GasFired,
        [JsonPropertyName("turbojet")]
        Turbojet,
        [JsonPropertyName("windturbine")]
        WindTurbine
    }
}
