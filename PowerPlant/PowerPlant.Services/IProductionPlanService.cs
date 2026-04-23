using PowerPlant.Services.DTOs;

namespace PowerPlant.Services
{
    public interface IProductionPlanService
    {
        List<PowerPlantAllocationDto> Calculate(ProductionPlanDto plan);
    }
}
