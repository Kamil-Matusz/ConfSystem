using ConfSystem.Modules.Agendas.Domain.Agendas.Entities;
using ConfSystem.Modules.Agendas.Domain.CallForPapers.Entities;
using ConfSystem.Modules.Agendas.Domain.Submissions.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL;

internal class AgendasDbContext : DbContext
{
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<CallForPapers> CallForPapers { get; set; }
        public DbSet<AgendaItem> AgendaItems { get; set; }
        public DbSet<AgendaTrack> AgendaTracks { get; set; }
        public DbSet<AgendaSlot> AgendaSlots { get; set; }


        public AgendasDbContext(DbContextOptions<AgendasDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("agendas");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
}