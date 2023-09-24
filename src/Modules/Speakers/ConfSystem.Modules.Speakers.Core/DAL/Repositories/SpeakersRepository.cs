using ConfSystem.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Speakers.Core.DAL.Repositories;

internal class SpeakersRepository : ISpeakersRepository
{
    private readonly SpeakersDbContext _speakersDbContext;
    private readonly DbSet<Speaker> _speakers;

    public SpeakersRepository(SpeakersDbContext speakersDbContext)
    {
        _speakersDbContext = speakersDbContext;
        _speakers = _speakersDbContext.Speakers;
    }

    public async Task<IReadOnlyList<Speaker>> GetAllSpeakersAsync() 
        => await _speakers.ToListAsync();

    public async Task<Speaker> GetSpeakerAsync(Guid id) 
        => await _speakers.SingleOrDefaultAsync(s => s.SpeakerId == id);

    public async Task<bool> ExistsAsync(Guid id) 
        => await _speakers.AnyAsync(s => s.SpeakerId == id);

    public async Task AddSpeakerAsync(Speaker speaker)
    {
        await _speakers.AddAsync(speaker);
        await _speakersDbContext.SaveChangesAsync();
    }

    public async Task UpdateSpeakerAsync(Speaker speaker)
    {
        _speakers.Update(speaker);
        await _speakersDbContext.SaveChangesAsync();
    }
}