using ConfSystem.Modules.Tickets.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfSystem.Modules.Tickets.Core.DAL.Configurations;

internal class TicketSaleConfiguration : IEntityTypeConfiguration<TicketSale>
{
    public void Configure(EntityTypeBuilder<TicketSale> builder)
    {
    }
}