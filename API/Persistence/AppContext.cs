using API.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence;

public class AppContext : IdentityDbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    
    public DbSet<UserEntity> AppUsers { get; set; }

    public DbSet<TimeOffEntity> TimeOffs { get; set; }
    
    public DbSet<ShiftEntity> Shifts { get; set; }
    
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