using ConfSystem.Modules.Conferences.Core.DTO;
using ConfSystem.Modules.Conferences.Core.Entities;
using ConfSystem.Modules.Conferences.Core.Exceptions;
using ConfSystem.Modules.Conferences.Core.Policies;
using ConfSystem.Modules.Conferences.Core.Repositories;

namespace ConfSystem.Modules.Conferences.Core.Services;

internal class HostService : IHostService
{
    private readonly IHostRepository _hostRepository;
    private readonly IHostDeletePolicy _hostDeletePolicy;

    public HostService(IHostRepository hostRepository, IHostDeletePolicy hostDeletePolicy)
    {
        _hostRepository = hostRepository;
        _hostDeletePolicy = hostDeletePolicy;
    }
    
    public async Task AddAsync(HostDto dto)
    {
        dto.HostId = Guid.NewGuid();
        await _hostRepository.AddAsync(new Host
        {
            HostId = dto.HostId,
            Name = dto.Name,
            Description = dto.Description
        });
    }

    public async Task<HostDetailsDto> GetAsync(Guid id)
    {
        var host = await _hostRepository.GetAsync(id);
        if (host is null)
        {
            return null;
        }

        var dto = Map<HostDetailsDto>(host);
        dto.Conferences = host.Conferences.Select(x => new ConferenceDto
        {
            ConferenceId = x.ConferenceId,
            HostId = x.HostId,
            HostName = x.Host.Name,
            From = x.From,
            To = x.To,
            Name = x.Name,
            Location = x.Location,
            LogoUrl = x.LogoUrl,
            ParticipantsLimit = x.ParticipantsLimit
        }).ToList();

        return dto;
    }

    public async Task<IReadOnlyList<HostDto>> GetAllAsync()
    {
        var hosts = await _hostRepository.GetAllAsync();
        return hosts.Select(Map<HostDto>).ToList();
    }

    public async Task UpdateAsync(HostDetailsDto dto)
    {
        var host = await _hostRepository.GetAsync(dto.HostId);
        if (host is null)
        {
            throw new HostNotFoundException(dto.HostId);
        }

        host.Name = dto.Name;
        host.Description = dto.Description;
        await _hostRepository.UpdateAsync(host);
    }

    public async Task DeleteAsync(Guid id)
    {
        var host = await _hostRepository.GetAsync(id);
        if (host is null)
        {
            throw new HostNotFoundException(id);
        }

        if (await _hostDeletePolicy.CanDeleteAsync(host) is false)
        {
            throw new CannotDeleteHostException(id);
        }
        await _hostRepository.DeleteAsync(host);
    }

    private static T Map<T>(Host host) where T : HostDto, new() => new T()
    {
        HostId = host.HostId,
        Name = host.Name,
        Description = host.Description,
    };
}