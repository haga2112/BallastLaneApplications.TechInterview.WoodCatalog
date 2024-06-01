using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("create-user")]
        public ActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (string.IsNullOrEmpty(name))
            //{
            //    var nameCannotBeEmptyMessage = "Name cannot be empty";
            //    _logger.LogError(nameCannotBeEmptyMessage);
            //    return BadRequest(nameCannotBeEmptyMessage);
            //}

            //if (string.IsNullOrEmpty(password))
            //{
            //    var passwordCannotBeEmptyMessage = "Name cannot be empty";
            //    _logger.LogError(passwordCannotBeEmptyMessage);
            //    return BadRequest(passwordCannotBeEmptyMessage);
            //}

            //User user = new User 
            //{ 
            //    Name = name,
            //    Password = password,
            //};

            _userService.AddUser(user);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<bool> Login(string id, string password)
        {
            (bool success, User? user) = _userService.LoginUser(id, password);

            if (success)
            {
                var jwtToken = new TokenGenerator().GenerateJwtToken(user!);

                return Ok(jwtToken);
            }

            return Unauthorized();
        }
    }
}
