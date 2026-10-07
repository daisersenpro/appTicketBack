namespace UDD.TicketsAPI.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Abierto";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}