using ClassWork.Models;

namespace ClassWork.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginRequest loginRequest);
        Task<AuthResponse> Registration(RegisterRequest registerRequest);
    }
}
