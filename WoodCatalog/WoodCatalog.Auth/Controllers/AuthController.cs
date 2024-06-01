using Microsoft.AspNetCore.Mvc;

namespace WoodCatalog.Auth.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpPost("create-user")]
        public ActionResult<bool> CreateUser(string username, string password)
        {
            return Ok(true);
        }

        [HttpPost("login")]
        public ActionResult<bool> Login(string username, string password)
        {
            return Ok(true);
        }
    }
}
