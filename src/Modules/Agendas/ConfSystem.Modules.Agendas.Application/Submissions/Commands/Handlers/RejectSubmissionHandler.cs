using ConfSystem.Modules.Agendas.Application.Exceptions;
using ConfSystem.Modules.Agendas.Domain.Submissions.Repositories;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Kernel;

namespace ConfSystem.Modules.Agendas.Application.Submissions.Commands.Handlers;

public class RejectSubmissionHandler : ICommandHandler<RejectSubmission>
{
    private readonly ISubmissionRepository _submissionRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public RejectSubmissionHandler(ISubmissionRepository submissionRepository, IDomainEventDispatcher domainEventDispatcher)
    {
        _submissionRepository = submissionRepository;
        _domainEventDispatcher = domainEventDispatcher;
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
    }
}