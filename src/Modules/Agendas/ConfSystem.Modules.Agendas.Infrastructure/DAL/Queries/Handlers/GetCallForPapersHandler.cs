using ConfSystem.Modules.Agendas.Application.CallForPapers.DTO;
using ConfSystem.Modules.Agendas.Application.CallForPapers.Queries;
using ConfSystem.Modules.Agendas.Domain.CallForPapers.Entities;
using ConfSystem.Modules.Agendas.Infrastructure.DAL.Mappings;
using ConfSystem.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetCallForPapersHandler : IQueryHandler<GetCallForPapers, CallForPapersDto>
{
    private readonly DbSet<CallForPapers> _callForPapers;

    public GetCallForPapersHandler(AgendasDbContext context)
        => _callForPapers = context.CallForPapers;

    public async Task<CallForPapersDto> HandleAsync(GetCallForPapers query)
        => await _callForPapers
            .AsNoTracking()
            .Where(cfp => cfp.ConferenceId == query.ConferenceId)
            .Select(cfp => cfp.AsDto())
            .SingleOrDefaultAsync();
}