using Contracts.Auth;

namespace Application.AuthModule.Services;

public interface IAuthService
{
    Task<UserDTO> RegisterAsync(RegisterDTO request);
    Task<UserDTO> LoginAsync(LoginDTO request);
    Task<UserDTO> RefreshAsync(UserDTO request);

}