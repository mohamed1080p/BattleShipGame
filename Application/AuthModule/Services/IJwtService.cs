using Domain.Models.AuthModule;

namespace Application.AuthModule.Services;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}