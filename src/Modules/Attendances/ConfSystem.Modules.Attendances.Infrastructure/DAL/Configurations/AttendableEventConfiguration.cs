using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Modules.Attendances.Domain.Types;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfSystem.Modules.Attendances.Infrastructure.DAL.Configurations;

internal class AttendableEventConfiguration : IEntityTypeConfiguration<AttendableEvent>
{
    public void Configure(EntityTypeBuilder<AttendableEvent> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AttendableEventId(x));

        builder.Property(x => x.ConferenceId)
            .HasConversion(x => x.Value, x => new ConferenceId(x));
    }
}