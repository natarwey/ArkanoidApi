using ArkanoidApi.Model;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace ArkanoidApi.DataBaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BallSkin> BallSkins { get; set; }
        public DbSet<BoughtSkins> BoughtSkins { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();

            modelBuilder.Entity<BoughtSkins>()
                .HasOne(bs => bs.User)
                .WithMany()
                .HasForeignKey(bs => bs.UserId);

            modelBuilder.Entity<BoughtSkins>()
                .HasOne(bs => bs.BallSkin)
                .WithMany()
                .HasForeignKey(bs => bs.SkinId);
        }
    }
}
