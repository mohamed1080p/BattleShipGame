using Application.AuthModule.Repositories;
using Domain.Models.AuthModule;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.AuthModule;

public class RefreshTokenRepository(AppDbContext _dbContext) : IRefreshTokenRepository
{
    public Task<RefreshToken?> GetByTokenAsync(string token) =>
        _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);

    public async Task AddAsync(RefreshToken refreshToken) =>
        await _dbContext.RefreshTokens.AddAsync(refreshToken);

    public void Revoke(RefreshToken refreshToken) =>
        _dbContext.RefreshTokens.Remove(refreshToken);

    public Task SaveChangesAsync() =>
        _dbContext.SaveChangesAsync();
}