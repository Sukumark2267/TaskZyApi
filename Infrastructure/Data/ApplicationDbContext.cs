using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Work.PostedBy → User
            modelBuilder.Entity<Work>()
                .HasOne(w => w.PostedUser)
                .WithMany() // No PostedWorks in User
                .HasForeignKey(w => w.PostedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Work.ConfirmedBy → User
            modelBuilder.Entity<Work>()
                .HasOne(w => w.ConfirmedUser)
                .WithMany() // No ConfirmedWorks in User
                .HasForeignKey(w => w.ConfirmedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // WorkConfirm.ConfirmedBy → User
            modelBuilder.Entity<WorkConfirm>()
                .HasOne(wc => wc.ConfirmedUser)
                .WithMany() // No collection in User
                .HasForeignKey(wc => wc.ConfirmedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // WorkConfirm.WorkId → Work
            modelBuilder.Entity<WorkConfirm>()
                .HasOne(wc => wc.Work)
                .WithMany() // No Confirmations collection in Work
                .HasForeignKey(wc => wc.WorkId)
                .OnDelete(DeleteBehavior.Restrict);

            // Default bools for User
            modelBuilder.Entity<User>()
                .Property(u => u.IsWorker)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .Property(u => u.IsFarmer)
                .HasDefaultValue(false);
        }





    }

}
