using Microsoft.AspNetCore.Mvc;
using RecipeProject.Application.Services;
using RecipeProject.Application.UseCases;
using RecipeProject.Application.Interfaces;
using RecipeProject.Domain.Entities;

namespace RecipeProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenService _jwtTokenService;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            // Normalmente usarías DI, aquí es simple para el ejemplo
            _jwtTokenService = new JwtTokenService(
                "clave_super_secreta", "RecipeApi", "RecipeApiUsers");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userRepository.GetByEmail(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new { token });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}