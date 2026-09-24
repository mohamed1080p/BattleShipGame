using Domain.Models.AuthModule;

namespace Application.AuthModule.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserByIdAsync(string userId);
}