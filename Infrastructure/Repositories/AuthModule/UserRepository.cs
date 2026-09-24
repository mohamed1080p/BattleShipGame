using Application.AuthModule.Repositories;
using Domain.Models.AuthModule;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.AuthModule;

public class UserRepository(AppDbContext _dbContext):IUserRepository
{
    public async Task<User?> GetUserByIdAsync(string userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if (user is null)
        {
            throw new Exception("User not found");
        }
        return user;
    }
}