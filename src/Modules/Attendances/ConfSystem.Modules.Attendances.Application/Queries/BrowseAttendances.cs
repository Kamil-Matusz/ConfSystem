using ConfSystem.Modules.Attendances.Application.DTO;
using ConfSystem.Shared.Abstractions.Queries;

namespace ConfSystem.Modules.Attendances.Application.Queries;

public class BrowseAttendances : IQuery<IReadOnlyList<AttendanceDto>>
{
    public Guid UserId { get; set; }
    public Guid ConferenceId { get; set; }
}