using ConfSystem.Modules.Agendas.Application.CallForPapers.DTO;
using ConfSystem.Modules.Agendas.Domain.CallForPapers.Entities;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Mappings;

internal static class CallForPapersExtensions
{
    public static CallForPapersDto AsDto(this CallForPapers dbModel)
        => new()
        {
            Id = dbModel.Id,
            ConferenceId = dbModel.ConferenceId,
            From = dbModel.From,
            To = dbModel.To,
            IsOpened = dbModel.IsOpened,
        };
}