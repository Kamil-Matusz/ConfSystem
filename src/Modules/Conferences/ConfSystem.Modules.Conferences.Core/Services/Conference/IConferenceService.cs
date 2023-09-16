using ConfSystem.Modules.Conferences.Core.DTO;

namespace ConfSystem.Modules.Conferences.Core.Services;

internal interface IConferenceService
{
    Task AddAsync(ConferenceDetailsDto dto);
    Task <ConferenceDetailsDto> GetAsync(Guid id);
    Task <IReadOnlyList<ConferenceDto>> GetAllAsync();
    Task UpdateAsync(ConferenceDetailsDto dto);
    Task DeleteAsync(Guid id);
}