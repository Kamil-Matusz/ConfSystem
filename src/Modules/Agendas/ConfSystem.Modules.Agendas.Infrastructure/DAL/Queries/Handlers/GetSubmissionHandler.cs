using ConfSystem.Modules.Agendas.Application.Submissions.DTO;
using ConfSystem.Modules.Agendas.Application.Submissions.Queries;
using ConfSystem.Modules.Agendas.Domain.Submissions.Entities;
using ConfSystem.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetSubmissionHandler : IQueryHandler<GetSubmission, SubmissionDto>
{
    private readonly DbSet<Submission> _submissions;

    public GetSubmissionHandler(AgendasDbContext agendasDbContext)
    {
        _submissions = agendasDbContext.Submissions;
    }

    public async Task<SubmissionDto> HandleAsync(GetSubmission query)
        => await _submissions
            .AsNoTracking()
            .Where(x => x.Id.Equals(query.Id))
            .Include(x => x.Speakers)
            .Select(x => new SubmissionDto
            {
                SubmissionId= x.Id,
                ConferenceId = x.ConferenceId,
                Title = x.Title,
                Description = x.Description,
                Level = x.Level,
                Status = x.Status,
                Tags = x.Tags,
                Speakers = x.Speakers.Select(s => new SpeakerDto
                {
                    SpeakerId = s.Id,
                    FullName = s.FullName
                })
            })
            .SingleOrDefaultAsync();
}