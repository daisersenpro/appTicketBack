using Microsoft.EntityFrameworkCore;
using UDD.TicketsAPI.Models;

namespace UDD.TicketsAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }
}