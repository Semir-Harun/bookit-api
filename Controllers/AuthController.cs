using BookingSystem.Data;
using BookingSystem.Dtos;
using BookingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly JwtTokenService _jwt;

    public AuthController(AppDbContext db, JwtTokenService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest("Email and password are required.");

        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == dto.Email);

        if (user is null)
            return Unauthorized("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials.");

        var roleName = user.Role?.RoleName ?? "User"; // "Admin" / "User" // Antar at user.Role finnes og har RoleName (eller at RoleId kan mappes til navn)
        var token = _jwt.CreateToken(userId: user.UserId, email: user.UserName, roleName: roleName);

        return Ok(new LoginResponse
        {
            Token = token,
            Email = user.UserName,
            Role = roleName
        });
    }
}