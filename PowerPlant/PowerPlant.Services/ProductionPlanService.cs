using PowerPlant.Services.DTOs;

namespace PowerPlant.Services
{
    public class ProductionPlanService : IProductionPlanService
    {
        public List<PowerPlantAllocationDto> Calculate(ProductionPlanDto plan)
        {
            // mocked result
            return new List<PowerPlantAllocationDto>
            {
                new() { Name = "windpark1", P = 90.0 },
                new() { Name = "windpark2", P = 21.6 },
                new() { Name = "gasfiredbig1", P = 460 },
                new() { Name = "gasfiredbig2", P = 338.4 },
                new() { Name = "gasfiredsomewhatsmaller", P = 0 },
                new() { Name = "tj1", P = 0 }
            };
        }
    }
}
