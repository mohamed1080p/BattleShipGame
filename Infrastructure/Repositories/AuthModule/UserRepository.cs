using Application.AuthModule.Repositories;
using Domain.Models.AuthModule;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.AuthModule;

public class UserRepository(AppDbContext _dbContext):IUserRepository
{
    public Task<User?> GetUserByIdAsync(Guid userId) =>
        _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

    public Task<User?> GetUserByEmailAsync(string email) =>
        _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

    public Task<bool> ExistsAsync(string email, string username) =>
        _dbContext.Users.AnyAsync(u => u.Email == email || u.Username == username);

    public async Task AddAsync(User user) =>
        await _dbContext.Users.AddAsync(user);

    public Task SaveChangesAsync() =>
        _dbContext.SaveChangesAsync();
}