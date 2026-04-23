using Microsoft.AspNetCore.Mvc;
using PowerPlant.Api.Requests;

namespace PowerPlant.Api.Controllers
{
    [ApiController]
    [Route("productionplan")]
    public class ProductionPlanController : ControllerBase
    {
        private readonly ILogger<ProductionPlanController> _logger;

        public ProductionPlanController(
            ILogger<ProductionPlanController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] ProductionPlanRequest request)
        {
            return Ok();
        }

    }
}
