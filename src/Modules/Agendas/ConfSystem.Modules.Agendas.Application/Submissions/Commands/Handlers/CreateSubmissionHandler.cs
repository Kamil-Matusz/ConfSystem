using System.Security.Cryptography;
using ConfSystem.Modules.Agendas.Application.Exceptions;
using ConfSystem.Modules.Agendas.Application.Mappers;
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

    public CreateSubmissionHandler(ISubmissionRepository submissionRepository, ISpeakerRepository speakerRepository, IDomainEventDispatcher domainEventDispatcher, IEventMapper eventMapper, IMessageBroker messageBroker)
    {
        _submissionRepository = submissionRepository;
        _speakerRepository = speakerRepository;
        _domainEventDispatcher = domainEventDispatcher;
        _eventMapper = eventMapper;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(CreateSubmission command)
    {
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