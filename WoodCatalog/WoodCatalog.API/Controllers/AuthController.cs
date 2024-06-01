using Microsoft.AspNetCore.Mvc;
using WoodCatalog.API.Helpers;
using WoodCatalog.Domain.Models;
using WoodCatalog.Domain.Services.Interfaces;

namespace WoodCatalog.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;

        public AuthController(ILogger<AuthController> logger,
                                IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("create-user")]
        public ActionResult<bool> CreateUser(string username, string password)
        {
            return Ok(true);
        }

        [HttpPost("login")]
        public ActionResult<bool> Login(string username, string password)
        {
            (bool success, User? user) = _userService.LoginUser(username, password);

            if (success)
            {
                var jwtToken = new TokenGenerator().GenerateJwtToken(user!);

                return Ok(jwtToken);
            }

            return Unauthorized();
        }
    }
}
