using BookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers;

[ApiController]
[Route("auth-debug")]
public class AuthDebugController : ControllerBase
{
    private readonly JwtTokenService _jwt;

    public AuthDebugController(JwtTokenService jwt)
    {
        _jwt = jwt;
    }

    [HttpGet("admin")]
    public IActionResult AdminToken()
    {
        var token = _jwt.CreateToken(userId: 1, userName: "admin", roleName: "Admin");
        return Ok(new { token });
    }

    [HttpGet("user")]
    public IActionResult UserToken()
    {
        var token = _jwt.CreateToken(userId: 2, userName: "user", roleName: "User");
        return Ok(new { token });
    }
}