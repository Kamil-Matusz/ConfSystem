using ConfSystem.Shared.Infrastructure.PostgreSQL;

namespace ConfSystem.Modules.Attendances.Infrastructure.DAL;

internal class AttendancesUnitOfWork : PostgresUnitOfWork<AttendancesDbContext>, IAttendancesUnitOfWork
{
    public AttendancesUnitOfWork(AttendancesDbContext dbContext) : base(dbContext)
    {
    }
}