using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }

        public DbSet<WorkConfirm> WorkConfirm { get; set; }
    }
}
