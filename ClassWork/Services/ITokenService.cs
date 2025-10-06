using ClassWork.Entities;

namespace ClassWork.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);

        string GenerateRefreshToken();
    }
}
