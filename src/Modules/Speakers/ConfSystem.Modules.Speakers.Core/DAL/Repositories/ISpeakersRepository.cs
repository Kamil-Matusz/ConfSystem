using ConfSystem.Modules.Speakers.Core.Entities;

namespace ConfSystem.Modules.Speakers.Core.DAL.Repositories;

internal interface ISpeakersRepository
{
    Task<IReadOnlyList<Speaker>> GetAllSpeakersAsync();
    Task<Speaker> GetSpeakerAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task AddSpeakerAsync(Speaker speaker);
    Task UpdateSpeakerAsync(Speaker speaker);
}