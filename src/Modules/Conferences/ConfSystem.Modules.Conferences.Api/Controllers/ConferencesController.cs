using ConfSystem.Modules.Conferences.Core.DTO;
using ConfSystem.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Conferences.Api.Controllers;

internal class ConferencesController : BaseController
{
    private readonly IConferenceService _conferenceService;

    public ConferencesController(IConferenceService conferenceService)
    {
        _conferenceService = conferenceService;
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ConferenceDetailsDto>> GetConference(Guid id) =>
        OkOrNotFound(await _conferenceService.GetAsync(id));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ConferenceDto>>> GetAllConferencesAsync() =>
        Ok(await _conferenceService.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult> CreateConferenceAsync(ConferenceDetailsDto dto)
    {
        await _conferenceService.AddAsync(dto);
        return CreatedAtAction(nameof(GetConference), new
        {
            id = dto.HostId,
        }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateConferenceAsync(Guid id, ConferenceDetailsDto dto)
    {
        dto.ConferenceId = id;
        await _conferenceService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteConferenceAsync(Guid id)
    {
        await _conferenceService.DeleteAsync(id);
        return NoContent();
    }
}