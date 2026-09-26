using Application.AuthModule.Repositories;
using Contracts.Auth;
using Domain.Models.AuthModule;

namespace Application.AuthModule.Services;

public class AuthService(IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService):IAuthService
{
    private const int RefreshTokenExpiryDays = 7;

    public async Task<UserDTO> RegisterAsync(RegisterDTO request)
    {
        if (await userRepository.ExistsAsync(request.Email, request.Username))
            throw new InvalidOperationException("Email or username already in use.");

        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Username = request.Username,
            PasswordHash = passwordHasher.Hash(request.Password)
        };

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

        return await IssueTokensAsync(user);
    }

    public async Task<UserDTO> LoginAsync(LoginDTO request)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email);

        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        return await IssueTokensAsync(user);
    }

    public async Task<UserDTO> RefreshAsync(UserDTO request)
    {
        var existingToken = await refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

        if (existingToken is null || !existingToken.IsActive)
            throw new UnauthorizedAccessException("Invalid or expired refresh token.");

        var user = await userRepository.GetUserByIdAsync(existingToken.UserId);
        if (user is null)
            throw new UnauthorizedAccessException("Invalid or expired refresh token.");

        refreshTokenRepository.Revoke(existingToken);
        await refreshTokenRepository.SaveChangesAsync();

        return await IssueTokensAsync(user);
    }
    private async Task<UserDTO> IssueTokensAsync(User user)
    {
        var accessToken = jwtService.GenerateAccessToken(user);
        var refreshTokenValue = jwtService.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshTokenValue,
            ExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays)
        };

        await refreshTokenRepository.AddAsync(refreshToken);
        await refreshTokenRepository.SaveChangesAsync();

        return new UserDTO
        {
            Username = user.Username,
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            UserId = user.Id
        };
    }
    
}