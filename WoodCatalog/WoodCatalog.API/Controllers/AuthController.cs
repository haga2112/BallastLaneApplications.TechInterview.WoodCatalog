using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ITokenService _tokenService;

        public AuthController(ILogger<AuthController> logger,
                                IUserService userService,
                                ITokenService tokenService)
        {
            _logger = logger;
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register(User user)
        {
            _userService.Register(user);

            return Ok(user.Id);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<bool> Login(string id, string password)
        {
            (bool success, User? user) = _userService.LoginUser(id, password);

            if (success)
            {
                var jwtToken = _tokenService.GenerateJwtToken(user!);

                return Ok(jwtToken);
            }

            return Unauthorized();
        }
    }
}
