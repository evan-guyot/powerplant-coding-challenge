namespace PowerPlant.Models
{
    public class ProductionPlan
    {
        public double Load { get; set; }
        public Fuels Fuels { get; set; }
        public List<PowerPlant> PowerPlants { get; set; }
    }
}
