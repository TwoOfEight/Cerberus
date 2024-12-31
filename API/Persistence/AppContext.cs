using API.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence;

public class Repository : IdentityDbContext
{
    public Repository(DbContextOptions<Repository> options) : base(options)
    {
    }

    public DbSet<UserModel> AppUsers { get; set; }

    public DbSet<TimeOffModel> TimeOffs { get; set; }

    public DbSet<ShiftModel> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TimeOffModel>()
            .HasOne(t => t.User)
            .WithMany(u => u.TimeOffs)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ShiftModel>()
            .HasOne(s => s.UserModel)
            .WithMany(u => u.Shifts)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}