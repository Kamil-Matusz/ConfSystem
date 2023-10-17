using ConfSystem.Modules.Agendas.Application.Submissions.DTO;
using ConfSystem.Shared.Abstractions.Queries;

namespace ConfSystem.Modules.Agendas.Application.Submissions.Queries;

public class GetSubmission : IQuery<SubmissionDto>
{
    public Guid Id { get; set; }
}