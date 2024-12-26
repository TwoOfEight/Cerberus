using API.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence;

public class Repository : IdentityDbContext
{
    public Repository(DbContextOptions<Repository> options) : base(options)
    {
    }

    /**
     * The IdentityDbContext already includes a Users property for IdentityUser.
     * To fix this:
     *      Use the new keyword to hide the base property explicitly.
     *      Alternatively, remove your Users declaration and rely on the inherited property.
    */
    public DbSet<UserEntity> AppUsers { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
}