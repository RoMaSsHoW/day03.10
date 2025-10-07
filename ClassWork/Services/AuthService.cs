using ClassWork.Abstractions;
using ClassWork.Entities;
using ClassWork.Models;

namespace ClassWork.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAppDbContext _context;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthService(IAppDbContext context, IUserService userService, ITokenService tokenService)
        {
            _context = context;
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Login(LoginRequest loginRequest)
        {
            var user = await _userService.GetByEmail(loginRequest.Email);
            if (user is null)
                throw new Exception("Such user does not exist");

            if (!user.Verify(loginRequest.Password))
                throw new Exception("Invalid password");

            var refreshToken = _tokenService.GenerateRefreshToken();
            user.ChangeRefreshToken(refreshToken);
            await _context.SaveChangesAsync();

            var accessToken = _tokenService.GenerateAccessToken(user);

            return new AuthResponse(accessToken, refreshToken);
        }

        public async Task<AuthResponse> Registration(RegisterRequest registerRequest)
        {
            var refreshToken = _tokenService.GenerateRefreshToken();

            var user = User.Registr(
                registerRequest.Name,
                registerRequest.Email,
                registerRequest.Password,
                refreshToken);

            var createdUser = await _userService.Add(user);
            var accessToken = _tokenService.GenerateAccessToken(createdUser);

            return new AuthResponse(accessToken, refreshToken);
        }
    }
}
