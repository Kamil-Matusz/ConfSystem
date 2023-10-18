using ConfSystem.Modules.Agendas.Application.CallForPapers.DTO;
using ConfSystem.Shared.Abstractions.Queries;

namespace ConfSystem.Modules.Agendas.Application.CallForPapers.Queries;

public class GetCallForPapers : IQuery<CallForPapersDto>
{
    public Guid ConferenceId { get; set; }
}