using Microsoft.EntityFrameworkCore;

namespace AssignmentTicketBooking.Models
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options)
            :base(options)
        {
                
        }

        public DbSet<SeatingSection> Tickets { get; set; }
    }
}
