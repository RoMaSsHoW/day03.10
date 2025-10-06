using ClassWork.Entities;
using ClassWork.Models;
using ClassWork.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user.Name == "123" && user.Password == "123")
            {
                var refreshToken = _tokenService.GenerateRefreshToken();

                var accessToken = _tokenService.GenerateAccessToken(user);

                return Ok(new AuthResponse(accessToken, refreshToken));
            }

            return Unauthorized("Invalid");
        }

        [HttpGet("get-some")]
        [Authorize]
        public IActionResult GetSome()
        {
            var result = new
            {
                success = true,
                message = "✅ Запрос выполнен успешно",
                data = new
                {
                    id = 1,
                    title = "Пример данных",
                    description = "Это демонстрационный результат авторизованного запроса",
                    timestamp = DateTime.UtcNow
                }
            };

            return Ok(result);
        }
    }
}
