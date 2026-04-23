using Microsoft.AspNetCore.Mvc;
using PowerPlant.Api.Requests;
using PowerPlant.Services;
using PowerPlant.Services.DTOs;

namespace PowerPlant.Api.Controllers
{
    [ApiController]
    [Route("productionplan")]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlanService _productionPlanService;

        public ProductionPlanController(
            IProductionPlanService productionPlanService)
        {
            _productionPlanService = productionPlanService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<PowerPlantAllocationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] ProductionPlanRequest request)
        {
            var dto = MapToDto(request);
            var result = _productionPlanService.Calculate(dto);

            return Ok(result);
        }

        private static ProductionPlanDto MapToDto(ProductionPlanRequest request)
        {
            return new ProductionPlanDto
            {
                Load = request.Load,
                Fuels = new FuelsDto
                {
                    Gas = request.Fuels.Gas,
                    Kerosine = request.Fuels.Kerosine,
                    Co2 = request.Fuels.Co2,
                    Wind = request.Fuels.Wind
                },
                PowerPlants = request.PowerPlants.Select(pp => new PowerPlantDto
                {
                    Name = pp.Name,
                    Type = pp.Type,
                    Efficiency = pp.Efficiency,
                    Pmin = pp.Pmin,
                    Pmax = pp.Pmax
                }).ToList()
            };
        }
    }
}
