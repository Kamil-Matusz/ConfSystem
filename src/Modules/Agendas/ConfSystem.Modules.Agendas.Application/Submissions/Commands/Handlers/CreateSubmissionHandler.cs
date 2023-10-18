using System.Security.Cryptography;
using ConfSystem.Modules.Agendas.Application.CallForPapers.Exceptions;
using ConfSystem.Modules.Agendas.Application.Exceptions;
using ConfSystem.Modules.Agendas.Application.Mappers;
using ConfSystem.Modules.Agendas.Domain.CallForPapers.Repositories;
using ConfSystem.Modules.Agendas.Domain.Submissions.Entities;
using ConfSystem.Modules.Agendas.Domain.Submissions.Repositories;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Kernel;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Modules.Agendas.Application.Submissions.Commands.Handlers;

public sealed class CreateSubmissionHandler : ICommandHandler<CreateSubmission>
{
    private readonly ISubmissionRepository _submissionRepository;
    private readonly ISpeakerRepository _speakerRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IEventMapper _eventMapper;
    private readonly IMessageBroker _messageBroker;
    private readonly ICallForPapersRepository _callForPapersRepository;

    public CreateSubmissionHandler(ISubmissionRepository submissionRepository, ISpeakerRepository speakerRepository, IDomainEventDispatcher domainEventDispatcher, IEventMapper eventMapper, IMessageBroker messageBroker, ICallForPapersRepository callForPapersRepository)
    {
        _submissionRepository = submissionRepository;
        _speakerRepository = speakerRepository;
        _domainEventDispatcher = domainEventDispatcher;
        _eventMapper = eventMapper;
        _messageBroker = messageBroker;
        _callForPapersRepository = callForPapersRepository;
    }

    public async Task HandleAsync(CreateSubmission command)
    {
        var callForPapers = await _callForPapersRepository.GetAsync(command.ConferenceId);
        if (callForPapers is null)
        {
            throw new CallForPapersNotFoundException(command.ConferenceId);
        }

        if (!callForPapers.IsOpened)
        {
            throw new CallForPapersClosedException(command.ConferenceId);
        }
        
        var speakerIds = command.SpeakerIds.Select(x => new AggregateId(x));
        var speakers = await _speakerRepository.BrowseAsync(speakerIds);

        if (speakers.Count() != speakerIds.Count())
        {
            throw new InvalidSpeakersNumberException(command.Id);
        }

        var submission = Submission.Create(command.Id, command.ConferenceId, command.Title, command.Description, command.Level, command.Tags, speakers.ToList());

        await _submissionRepository.AddAsync(submission);
        await _domainEventDispatcher.DispatchAsync(submission.Events.ToArray());
        
        var events = _eventMapper.MapAll(submission.Events);
        await _messageBroker.PublishAsync(events.ToArray());
    }
}