using ConfSystem.Modules.Agendas.Domain.Agendas.Entities;
using ConfSystem.Shared.Abstractions.Kernel.Types;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Services;

public interface IAgendaTracksDomainService
{
    Task AssignAgendaItemAsync(AgendaTrack agendaTrack, EntityId agendaSlotId, EntityId agendaItemId);
}