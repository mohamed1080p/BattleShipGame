using Domain.Models.AuthModule;

namespace Application.AuthModule.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> ExistsAsync(string email, string username);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}