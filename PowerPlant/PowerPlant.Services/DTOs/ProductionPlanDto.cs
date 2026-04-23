namespace PowerPlant.Services.DTOs
{
    public class ProductionPlanDto
    {
        public double Load { get; set; }
        public FuelsDto Fuels { get; set; }
        public List<PowerPlantDto> PowerPlants { get; set; }
    }
}
