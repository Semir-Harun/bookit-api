namespace BookingSystem.Models;

public class User
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; } = null!;
    public List<Booking> Bookings { get; set; } = new();
}