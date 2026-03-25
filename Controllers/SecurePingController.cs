using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers;

[ApiController]
[Route("secure")]
public class SecurePingController : ControllerBase
{
    [HttpGet("ping")]
    [Authorize]
    public IActionResult Ping() => Ok("pong");

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly() => Ok("admin ok");

    [HttpGet("user")]
    [Authorize(Roles = "User")]
    public IActionResult UserOnly() => Ok("user ok");
}