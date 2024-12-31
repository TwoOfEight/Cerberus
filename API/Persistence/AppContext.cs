using API.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence;

public class Repository : IdentityDbContext
{
    public Repository(DbContextOptions<Repository> options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<Break> Breaks { get; set; }

    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Break>()
            .HasOne(t => t.User)
            .WithMany(u => u.TimeOffs)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Shift>()
            .HasOne(s => s.AppUser)
            .WithMany(u => u.Shifts)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}