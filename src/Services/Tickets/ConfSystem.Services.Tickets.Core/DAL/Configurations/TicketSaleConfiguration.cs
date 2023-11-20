using ConfSystem.Services.Tickets.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfSystem.Services.Tickets.Core.DAL.Configurations;

internal class TicketSaleConfiguration : IEntityTypeConfiguration<TicketSale>
{
    public void Configure(EntityTypeBuilder<TicketSale> builder)
    {
    }
}