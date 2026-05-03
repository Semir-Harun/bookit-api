namespace BookingSystem.Models;

public class Room
{
    public int RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public int Kapasitet { get; set; }
    public string Sted { get; set; } = string.Empty;
    public List<Booking> Bookings { get; set; } = new();
}