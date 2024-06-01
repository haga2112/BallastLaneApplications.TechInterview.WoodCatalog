using Microsoft.AspNetCore.Mvc;
using WoodCatalog.API.Helpers;

namespace WoodCatalog.API.Controllers
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
            // TODO: get do usuário

            // TODO: comparar hash da senha
            if (true)
            {
                var systemUser = new SystemUser();
                systemUser.Id = Guid.NewGuid();
                systemUser.Name = username;

                var jwtToken = string.Empty;
                //new TokenGenerator().GenerateJwtToken(systemUser);

                return Ok(jwtToken);
            }

            return Unauthorized();
        }
    }
}
