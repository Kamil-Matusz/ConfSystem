using ConfSystem.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Users.Core.DAL;

public sealed class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
        
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}