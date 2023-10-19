using ConfSystem.Modules.Agendas.Application.Agendas.Types;
using ConfSystem.Modules.Agendas.Domain.Agendas.Entities;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Configurations;

public class AgendaSlotConfiguration : IEntityTypeConfiguration<AgendaSlot>
{
    public void Configure(EntityTypeBuilder<AgendaSlot> builder)
    {
        builder.HasKey(s => s.Id);

        builder
            .Property(s => s.Id)
            .HasConversion(id => id.Value, id => new EntityId(id));
            
        builder
            .HasDiscriminator<string>("Type")
            .HasValue<PlaceholderAgendaSlot>(AgendaSlotType.Placeholder)
            .HasValue<RegularAgendaSlot>(AgendaSlotType.Regular);
    }
}