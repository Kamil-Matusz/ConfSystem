using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Modules.Attendances.Domain.Types;
using ConfSystem.Shared.Abstractions.Kernel.Types;

namespace ConfSystem.Modules.Attendances.Domain.Repositories;

public interface IParticipantsRepository
{
    Task<Participant> GetParticipantsAsync(ParticipantId id);
    Task<Participant> GetParticipantsAsync(ConferenceId conferenceId, UserId userId);
    Task AddParticipantsAsync(Participant participant);
    Task UpdateParticipantsAsync(Participant participant);
}