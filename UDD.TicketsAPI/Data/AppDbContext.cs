using Microsoft.EntityFrameworkCore;
using UDD.TicketsAPI.Models;

namespace UDD.TicketsAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=UDD_TicketsDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}