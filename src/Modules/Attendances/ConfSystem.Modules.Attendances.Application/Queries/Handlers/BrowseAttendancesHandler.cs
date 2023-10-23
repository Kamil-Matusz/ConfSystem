using ConfSystem.Modules.Attendances.Application.Clients.Agendas;
using ConfSystem.Modules.Attendances.Application.Clients.Agendas.DTO;
using ConfSystem.Modules.Attendances.Application.DTO;
using ConfSystem.Modules.Attendances.Domain.Repositories;
using ConfSystem.Shared.Abstractions.Queries;

namespace ConfSystem.Modules.Attendances.Application.Queries.Handlers;

internal sealed class BrowseAttendancesHandler : IQueryHandler<BrowseAttendances, IReadOnlyList<AttendanceDto>>
{
    private readonly IParticipantsRepository _participantsRepository;
    private readonly IAgendasApiClient _agendasApiClient;

    public BrowseAttendancesHandler(IParticipantsRepository participantsRepository,
        IAgendasApiClient agendasApiClient)
    {
        _participantsRepository = participantsRepository;
        _agendasApiClient = agendasApiClient;
    }

    public async Task<IReadOnlyList<AttendanceDto>> HandleAsync(BrowseAttendances query)
    {
        var participant = await _participantsRepository.GetParticipantsAsync(query.ConferenceId, query.UserId);
        if (participant is null)
        {
            return null;
        }

        var attendances = new List<AttendanceDto>();
        var tracks = await _agendasApiClient.GetAgendaAsync(query.ConferenceId);
        var slots = tracks.SelectMany(x => x.Slots.OfType<RegularAgendaSlotDto>()).ToArray();
        foreach (var attendance in participant.Attendances)
        {
            var slot = slots.Single(x => x.AgendaItem.Id == attendance.AttendableEventId);
            attendances.Add(new AttendanceDto
            {
                ConferenceId = query.ConferenceId,
                EventId = slot.Id,
                From = attendance.From,
                To = attendance.To,
                Title = slot.AgendaItem.Title,
                Description = slot.AgendaItem.Description,
                Level = slot.AgendaItem.Level
            });
        }

        return attendances;
    }
}