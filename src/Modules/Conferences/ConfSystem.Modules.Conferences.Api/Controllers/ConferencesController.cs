using ConfSystem.Modules.Conferences.Core.DTO;
using ConfSystem.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Conferences.Api.Controllers;

[Authorize]
internal class ConferencesController : BaseController
{
    private const string Policy = "conferences";
    private readonly IConferenceService _conferenceService;

    public ConferencesController(IConferenceService conferenceService)
    {
        _conferenceService = conferenceService;
    }


    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ConferenceDetailsDto>> GetConference(Guid id) =>
        OkOrNotFound(await _conferenceService.GetAsync(id));

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<IReadOnlyList<ConferenceDto>>> GetAllConferencesAsync() =>
        Ok(await _conferenceService.GetAllAsync());

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> CreateConferenceAsync(ConferenceDetailsDto dto)
    {
        await _conferenceService.AddAsync(dto);
        return CreatedAtAction(nameof(GetConference), new
        {
            id = dto.HostId,
        }, null);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> UpdateConferenceAsync(Guid id, ConferenceDetailsDto dto)
    {
        dto.ConferenceId = id;
        await _conferenceService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> DeleteConferenceAsync(Guid id)
    {
        await _conferenceService.DeleteAsync(id);
        return NoContent();
    }
}