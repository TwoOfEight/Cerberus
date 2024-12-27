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

        // // Configure one-to-one relationship
        // modelBuilder.Entity<EmployeeEntity>()
        //     .HasOne(e => e.User)
        //     .WithOne(u => u.Employee)
        //     .HasForeignKey<EmployeeEntity>(e => e.UserId)
        //     .OnDelete(DeleteBehavior.Cascade); // Cascade delete ensures cleanup
    }
}