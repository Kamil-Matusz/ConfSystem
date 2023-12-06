using ConfSystem.Modules.Conferences.Core.DAL;
using ConfSystem.Modules.Users.Core.DAL;
using ConfSystem.Shared.Tests;

namespace ConfSystem.Modules.Users.Tests.Integration.Common;

public class TestUsersDbContext : IDisposable
{
    public UsersDbContext DbContext { get; }

    public TestUsersDbContext()
    {
        DbContext = new UsersDbContext(DbHelper.GetOptions<UsersDbContext>());
    }

    public void Dispose()
    {
        DbContext?.Database.EnsureDeleted();
        DbContext?.Dispose();
    }
}