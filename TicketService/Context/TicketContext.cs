using Microsoft.EntityFrameworkCore;
using TicketService.Models;

namespace TicketService.Context
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
