using PowerPlant.Models;
using PowerPlant.Services.DTOs;

namespace PowerPlant.Services
{
    public class ProductionPlanService : IProductionPlanService
    {
        public List<PowerPlantAllocationDto> Calculate(ProductionPlanDto plan)
        {

            var plantInfos = new List<(PowerPlantDto Plant, double CostPerMWh, double EffectivePmax)>();

            #region calculate_compute_cost_and_pmax

            // Compute the cost and effective pmax for each plant

            foreach (var plant in plan.PowerPlants)
            {
                double cost;
                double effectivePmax;

                switch (plant.Type)
                {
                    case PowerPlantType.WindTurbine:
                        cost = 0; // Wind Turbines do not consume Fuel
                        effectivePmax = Math.Round(plant.Pmax * (plan.Fuels.Wind / 100.0), 1);
                        break;
                    case PowerPlantType.GasFired:
                        cost = plan.Fuels.Gas / plant.Efficiency;
                        effectivePmax = plant.Pmax;
                        break;
                    case PowerPlantType.Turbojet:
                        cost = plan.Fuels.Kerosine / plant.Efficiency;
                        effectivePmax = plant.Pmax;
                        break;
                    default:
                        throw new Exception($"Unknown PowerPlant type: {plant.Type}");
                }

                plantInfos.Add((plant, cost, effectivePmax));
            }

            #endregion

            // Sorted to prepare the allocation
            plantInfos = plantInfos
                .OrderBy(p => p.CostPerMWh)
                .ThenByDescending(p => p.EffectivePmax)
                .ToList();

            #region calculate_load_allocation


            var allocations = new Dictionary<string, double>();
            var remaining = plan.Load;

            // Allocate load
            foreach (var plantInfo in plantInfos)
            {
                var plant = plantInfo.Plant;
                var effectivePmax = plantInfo.EffectivePmax;

                if (remaining <= 0 || effectivePmax == 0)
                {
                    allocations[plant.Name] = 0;
                    continue;
                }

                if (remaining < plant.Pmin)
                {
                    var deficit = Math.Round(plant.Pmin - remaining, 1);
                    var freed = false;

                    foreach (var allocatedName in allocations.Keys.ToList())
                    {
                        var allocatedPlant = plan.PowerPlants.First(p => p.Name == allocatedName);
                        var newAllocation = Math.Round(allocations[allocatedName] - deficit, 1);

                        if (newAllocation >= allocatedPlant.Pmin && newAllocation >= 0)
                        {
                            allocations[allocatedName] = newAllocation;
                            remaining = Math.Round(remaining + deficit, 1);
                            freed = true;
                            break;
                        }
                    }

                    if (!freed)
                    {
                        allocations[plant.Name] = 0;
                        continue;
                    }
                }

                var production = Math.Round(Math.Min(remaining, effectivePmax), 1);

                if (production < plant.Pmin)
                {
                    allocations[plant.Name] = 0;
                    continue;
                }

                allocations[plant.Name] = production;
                remaining = Math.Round(remaining - production, 1);
            }

            #endregion

            return allocations.Select(kvp => new PowerPlantAllocationDto
            {
                Name = kvp.Key,
                P = kvp.Value
            }).ToList();
        }
    }
}
