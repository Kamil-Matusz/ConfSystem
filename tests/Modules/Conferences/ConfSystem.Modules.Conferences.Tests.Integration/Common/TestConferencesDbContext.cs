using ConfSystem.Modules.Conferences.Core.DAL;
using ConfSystem.Shared.Tests;

namespace ConfSystem.Modules.Conferences.Tests.Integration.Common;

public class TestConferencesDbContext : IDisposable
{
    public ConferencesDbContext DbContext { get; }

    public TestConferencesDbContext()
    {
        DbContext = new ConferencesDbContext(DbHelper.GetOptions<ConferencesDbContext>());
    }

    public void Dispose()
    {
        DbContext?.Database.EnsureDeleted();
        DbContext?.Dispose();
    }
}