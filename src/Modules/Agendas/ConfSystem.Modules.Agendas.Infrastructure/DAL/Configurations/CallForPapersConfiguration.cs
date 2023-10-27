using ConfSystem.Modules.Agendas.Domain.CallForPapers.Entities;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Configurations;

internal class CallForPapersConfiguration : IEntityTypeConfiguration<CallForPapers>
{
    public void Configure(EntityTypeBuilder<CallForPapers> builder)
    {
        builder.HasKey(s => s.Id);
            
        builder
            .Property(s => s.Id)
            .HasConversion(id => id.Value, id => new AggregateId(id));
            
        builder
            .Property(s => s.ConferenceId)
            .HasConversion(id => id.Value, id => new ConferenceId(id));

        builder
            .Property(cfp => cfp.Version)
            .IsConcurrencyToken();
    }
}