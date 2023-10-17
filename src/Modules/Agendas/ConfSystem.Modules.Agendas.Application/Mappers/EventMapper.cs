using ConfSystem.Modules.Agendas.Application.Submissions.Events;
using ConfSystem.Modules.Agendas.Domain.Submissions.Const;
using ConfSystem.Modules.Agendas.Domain.Submissions.Events;
using ConfSystem.Shared.Abstractions.Kernel;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Modules.Agendas.Application.Mappers;

public class EventMapper : IEventMapper
{
    public IMessage Map(IDomainEvent @event)
        => @event switch
        {
            SubmissionAdded e => new SubmissionCreated(e.Submission.Id),
            SubmissionStatusChanged { Status: SubmissionStatus.Approved } e => new SubmissionApproved(e.Submission.Id),
            SubmissionStatusChanged { Status: SubmissionStatus.Rejected } e => new SubmissionRejected(e.Submission.Id),
            _ => null
        };

    public IEnumerable<IMessage> MapAll(IEnumerable<IDomainEvent> events)
        => events.Select(Map);
}