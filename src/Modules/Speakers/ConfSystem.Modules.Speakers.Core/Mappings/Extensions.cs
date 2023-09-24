using ConfSystem.Modules.Speakers.Core.DTO;
using ConfSystem.Modules.Speakers.Core.Entities;

namespace ConfSystem.Modules.Speakers.Core.Mappings;

internal static class Extensions
{
    public static SpeakerDto AsDto(this Speaker entity)
        => new()
        {
            SpeakerId = entity.SpeakerId,
            Email = entity.Email,
            FullName = entity.FullName,
            Bio = entity.Bio,
            AvatarUrl = entity.AvatarUrl
        };
        
    public static Speaker AsEntity(this SpeakerDto dto)
        => new()
        {
            SpeakerId = dto.SpeakerId,
            Email = dto.Email,
            FullName = dto.FullName,
            Bio = dto.Bio,
            AvatarUrl = dto.AvatarUrl
        };
}