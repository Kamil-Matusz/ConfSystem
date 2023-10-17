using ConfSystem.Modules.Agendas.Domain.Submissions.Entities;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Configurations;

internal class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
{
    public void Configure(EntityTypeBuilder<Speaker> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder
            .Property(x => x.Version)
            .IsConcurrencyToken();
    }
}