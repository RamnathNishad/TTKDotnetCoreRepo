using Microsoft.EntityFrameworkCore;

namespace MyFirstMVCoreApp.Models
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set;}
    }
}
