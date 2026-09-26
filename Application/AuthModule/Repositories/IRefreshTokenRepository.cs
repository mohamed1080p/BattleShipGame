using Domain.Models.AuthModule;

namespace Application.AuthModule.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task AddAsync(RefreshToken refreshToken);
    void Revoke(RefreshToken refreshToken);
    Task SaveChangesAsync();

}