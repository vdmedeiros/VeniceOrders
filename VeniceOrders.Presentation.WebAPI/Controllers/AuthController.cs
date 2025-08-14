using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using VeniceOrders.Application.Interfaces;

namespace VeniceOrders.Presentation.WebAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            //Validação Fake
            if (request.Username == "admin" && request.Password == "123")
            {
                var token = _tokenService.GenerateToken(request.Username, "Admin");
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}

public record LoginRequest(string Username, string Password);
