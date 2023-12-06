using ConfSystem.Modules.Attendances.Infrastructure.DAL;
using ConfSystem.Shared.Tests;

namespace ConfSystem.Modules.Attandences.Tests.Integration.Common;

public class TestAttendancesDbContext : IDisposable
{
    public AttendancesDbContext DbContext { get; }

    public TestAttendancesDbContext()
    {
        DbContext = new AttendancesDbContext(DbHelper.GetOptions<AttendancesDbContext>());
    }

    public void Dispose()
    {
        DbContext?.Database.EnsureDeleted();
        DbContext?.Dispose();
    }
}