using Application.AuthModule.Services;
using Contracts.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BattleShipGame.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO request)
    {
        var result = await authService.RegisterAsync(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO request)
    {
        var result = await authService.LoginAsync(request);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<UserDTO>> Refresh(RefreshRequestDTO request)
    {
        var result = await authService.RefreshAsync(request);
        return Ok(result);
    }
}