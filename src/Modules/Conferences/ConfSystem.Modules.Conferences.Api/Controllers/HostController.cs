using ConfSystem.Modules.Conferences.Core.DTO;
using ConfSystem.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ConfSystem.Modules.Conferences.Api.Controllers;

[Authorize(Policy = Policy)]
internal class HostController : BaseController
{
    private const string Policy = "hosts";
    private readonly IHostService _hostService;

    public HostController(IHostService hostService)
    {
        _hostService = hostService;
    }

    [AllowAnonymous]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<HostDetailsDto>> GetHost(Guid id) => OkOrNotFound(await _hostService.GetAsync(id));

    [AllowAnonymous]
    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<HostDto>>> GetAllHostsAsync() => Ok(await _hostService.GetAllAsync());

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [HttpPost]
    public async Task<ActionResult> CreateHostAsync(HostDto dto)
    {
        await _hostService.AddAsync(dto);
        return CreatedAtAction(nameof(GetHost), new
        {
            id = dto.HostId,
        }, null);
    }
    
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateHostAsync(Guid id, HostDetailsDto dto)
    {
        dto.HostId = id;
        await _hostService.UpdateAsync(dto);
        return NoContent();
    }
    
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteHostAsync(Guid id)
    {
        await _hostService.DeleteAsync(id);
        return NoContent();
    }
    
}