using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HABManagement.Models;

namespace HABManagement.Data
{
    public class HABManagementContext : DbContext
    {
        public HABManagementContext (DbContextOptions<HABManagementContext> options)
            : base(options)
        {
        }

        public DbSet<HABManagement.Models.Food> Food { get; set; } = default!;

        public DbSet<HABManagement.Models.Kakei> Kakei { get; set; }

        public DbSet<HABManagement.Models.SList> SList { get; set; }
    }
}
