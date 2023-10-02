using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScaffFoldCRUDDemo.Models;

namespace ScaffFoldCRUDDemo.Data
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext (DbContextOptions<EmpDbContext> options)
            : base(options)
        {
        }

        public DbSet<ScaffFoldCRUDDemo.Models.Employee> Employee { get; set; } = default!;
    }
}
