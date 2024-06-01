using Microsoft.AspNetCore.Mvc;

namespace WoodCatalog.API.Controllers
{
    [ApiController]
    [Route("wood-catalog")]
    public class WoodController : ControllerBase
    {
        private readonly ILogger<WoodController> _logger;

        public WoodController(ILogger<WoodController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public ActionResult<string> Get(int id)
        {
            return Ok();
        }

        [HttpPost()]
        public ActionResult<string> Add(string name)
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<string> Update(int id, string name)
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<string> Delete(int id)
        {
            return Ok();
        }
    }
}
