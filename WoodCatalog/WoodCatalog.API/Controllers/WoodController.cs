using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoodCatalog.Domain.Models;
using WoodCatalog.Domain.Services.Interfaces;

namespace WoodCatalog.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("wood-catalog")]
    public class WoodController : ControllerBase
    {
        private readonly ILogger<WoodController> _logger;
        private readonly IWoodService _woodService;

        public WoodController(ILogger<WoodController> logger,
                                IWoodService woodService)
        {
            _logger = logger;
            _woodService = woodService;
        }

        [HttpGet()]
        public IActionResult GetWoodById(string id)
        {
            var wood = _woodService.GetWoodById(id);

            if (wood == null)
            {
                return NotFound();
            }
            return Ok(wood);
        }
        
        [HttpGet("get-all")]
        public IActionResult GetAllWoods()
        {
            var woods = _woodService.GetAllWoods();
            return Ok(woods);
        }

        [HttpPost()]
        public IActionResult AddWood(Wood wood)
        {
            _woodService.AddWood(wood);
            return Ok(wood);
        }

        [HttpPut]
        public IActionResult UpdateWood(Wood wood)
        {
            var woodToUpdate = _woodService.GetWoodById(wood.Id);
            if (woodToUpdate == null)
            {
                return NotFound();
            }
            _woodService.UpdateWood(wood);
            return Ok(wood);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var wood = _woodService.DeleteWood(id);
            if (wood == null)
            {
                return NotFound();
            }
            return Ok(wood);
        }
    }
}
