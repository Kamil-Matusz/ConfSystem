using ConfSystem.Modules.Conferences.Core.DTO;

namespace ConfSystem.Modules.Conferences.Core.Services;

internal interface IHostService
{
    Task AddAsync(HostDto dto);
    Task <HostDetailsDto> GetAsync(Guid id);
    Task <IReadOnlyList<HostDto>> GetAllAsync();
    Task UpdateAsync(HostDetailsDto dto);
    Task DeleteAsync(Guid id);

}