using ConfSystem.Modules.Agendas.Application.Exceptions;
using ConfSystem.Modules.Agendas.Application.Mappers;
using ConfSystem.Modules.Agendas.Domain.Submissions.Repositories;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Kernel;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Modules.Agendas.Application.Submissions.Commands.Handlers;

public class RejectSubmissionHandler : ICommandHandler<RejectSubmission>
{
    private readonly ISubmissionRepository _submissionRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IEventMapper _eventMapper;
    private readonly IMessageBroker _messageBroker;

    public RejectSubmissionHandler(ISubmissionRepository submissionRepository, IDomainEventDispatcher domainEventDispatcher, IEventMapper eventMapper, IMessageBroker messageBroker)
    {
        _submissionRepository = submissionRepository;
        _domainEventDispatcher = domainEventDispatcher;
        _eventMapper = eventMapper;
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(RejectSubmission command)
    {
        var submission = await _submissionRepository.GetAsync(command.Id);

        if (submission is null)
        {
            throw new SubmissionNotFoundException(command.Id);
        }
        
        submission.Reject();
        await _submissionRepository.UpdateAsync(submission);
        await _domainEventDispatcher.DispatchAsync(submission.Events.ToArray());
        
        var events = _eventMapper.MapAll(submission.Events);
        await _messageBroker.PublishAsync(events.ToArray());
    }
}