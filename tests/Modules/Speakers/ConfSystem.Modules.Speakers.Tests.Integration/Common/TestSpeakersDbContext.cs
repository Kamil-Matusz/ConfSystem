using ConfSystem.Modules.Speakers.Core.DAL;
using ConfSystem.Shared.Tests;

namespace ConfSystem.Modules.Speakers.Tests.Integration.Common;

public class TestSpeakersDbContext : IDisposable
{
    public SpeakersDbContext DbContext { get; }

    public TestSpeakersDbContext()
    {
        DbContext = new SpeakersDbContext(DbHelper.GetOptions<SpeakersDbContext>());
    }
    
    public void Dispose()
    {
        DbContext?.Database.EnsureDeleted();
        DbContext?.Dispose();
    }
}