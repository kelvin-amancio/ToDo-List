using Microsoft.AspNetCore.Mvc;
using ToDoApi.Repositories.Interfaces;
using ToDoApi.Services;
using ToDoApi.ViewModels;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserRepository userRepository, ITokenService tokenService) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            var result = await _userRepository.RegisterAsync(registerUserViewModel);

            if (result.Data is not null)
                return Ok();

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            var result = await _userRepository.LoginAsync(userViewModel);

            if (result.Data is not null)
            {
                var token = _tokenService.Create(result.Data);
                return Ok(new { Token = token });
            }

            return NotFound(result);
        }
    }
}
