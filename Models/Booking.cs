namespace BookingSystem.Models;

public class Booking
{
    public int BookingId { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public User? User { get; set; }
    public Room? Room { get; set; }
}