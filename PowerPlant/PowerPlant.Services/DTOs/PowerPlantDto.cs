using PowerPlant.Models;

namespace PowerPlant.Services.DTOs
{
    public class PowerPlantDto
    {
        public string Name { get; set; }
        public PowerPlantType Type { get; set; }
        public double Efficiency { get; set; }
        public double Pmin { get; set; }
        public double Pmax { get; set; }
    }
}
