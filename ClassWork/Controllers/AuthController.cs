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
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authService.Login(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _authService.Registration(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
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
