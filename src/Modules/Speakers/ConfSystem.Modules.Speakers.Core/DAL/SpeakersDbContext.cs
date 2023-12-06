using ConfSystem.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Speakers.Core.DAL;

public sealed class SpeakersDbContext : DbContext
{
    public DbSet<Speaker> Speakers { get; set; }
        
    public SpeakersDbContext(DbContextOptions<SpeakersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("speakers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}