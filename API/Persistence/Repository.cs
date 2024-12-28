using API.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence;

public class Repository : IdentityDbContext
{
    public Repository(DbContextOptions<Repository> options) : base(options) { }
    
    public DbSet<UserEntity> AppUsers { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure one-to-many relationship for TimeOffEntity
        modelBuilder.Entity<TimeOffEntity>()
            .HasOne(t => t.User)
            .WithMany(u => u.TimeOffs)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // Cascade delete when user is deleted

        // Configure one-to-many relationship for ShiftEntity
        modelBuilder.Entity<ShiftEntity>()
            .HasOne(s => s.User)
            .WithMany(u => u.Shifts)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // Cascade delete when user is deleted
    }
}