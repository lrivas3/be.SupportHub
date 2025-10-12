using Asp.Versioning;
using HelpPoint.Infrastructure.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpPoint.Features.Users;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion("1.0")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] RegisterDto request)
    {
        var response = await userService.CreateUser(request);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProfile()
    {
        var user = await userService.GetUserProfile();
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> ListUsers()
    {
        var result = await userService.ListUsers();
        return Ok(result);
    }
}
