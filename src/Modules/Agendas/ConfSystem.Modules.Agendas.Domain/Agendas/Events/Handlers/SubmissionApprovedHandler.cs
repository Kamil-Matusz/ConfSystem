using ConfSystem.Modules.Agendas.Domain.Agendas.Entities;
using ConfSystem.Modules.Agendas.Domain.Agendas.Repositories;
using ConfSystem.Modules.Agendas.Domain.Submissions.Const;
using ConfSystem.Modules.Agendas.Domain.Submissions.Events;
using ConfSystem.Shared.Abstractions.Kernel;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Events.Handlers;

internal sealed class SubmissionApprovedHandler : IDomainEventHandler<SubmissionStatusChanged>
{
    private readonly IAgendaItemsRepository _agendaItemsRepository;

    public SubmissionApprovedHandler(IAgendaItemsRepository agendaItemsRepository)
        => _agendaItemsRepository = agendaItemsRepository;

    public async Task HandleAsync(SubmissionStatusChanged domainEvent)
    {
        if (domainEvent.Status is SubmissionStatus.Rejected)
        {
            return;
        }

        var submission = domainEvent.Submission;
        var agendaItem = await _agendaItemsRepository.GetAsync(submission.Id);
        if (agendaItem is not null)
        {
            return;
        }

        agendaItem = AgendaItem.Create(submission.Id, submission.ConferenceId, submission.Title,
            submission.Description, submission.Level, submission.Tags, submission.Speakers.ToList());

        await _agendaItemsRepository.AddAsync(agendaItem);
    }
}