using ConfSystem.Modules.Speakers.Core.DTO;
using ConfSystem.Modules.Speakers.Core.Services;
using ConfSystem.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Speakers.Api.Controllers;

internal class SpeakersController : BaseController
{
    private readonly ISpeakersService _speakersService;
    private readonly IContext _context;

    public SpeakersController(ISpeakersService speakersService, IContext context)
    {
        _speakersService = speakersService;
        _context = context;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SpeakerDto>> GetSpeaker(Guid id) 
        =>  OkOrNotFound(await _speakersService.GetSpeakerAsync(id));
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpeakerDto>>> GetAllSpeakers() 
        => Ok(await _speakersService.GetAllSpeakersAsync());

    [HttpPost]
    public async Task<ActionResult> CreateNewSpeaker(SpeakerDto speaker)
    {
        await _speakersService.CreateSpeakerAsync(speaker);
        return CreatedAtAction(nameof(GetSpeaker), new
        {
            id = speaker.SpeakerId,
        }, null);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateSpeaker(Guid id, SpeakerDto speaker)
    {
        speaker.SpeakerId = _context.Identity.Id;
        await _speakersService.UpdateSpeakerAsync(speaker);
        return NoContent();
    }
}