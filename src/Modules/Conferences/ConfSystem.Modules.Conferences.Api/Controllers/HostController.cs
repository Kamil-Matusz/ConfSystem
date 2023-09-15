using ConfSystem.Modules.Conferences.Core.DTO;
using ConfSystem.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ConfSystem.Modules.Conferences.Api.Controllers;

[Route("conferences-module" + "[controller]")]
[ApiController]
internal class HostController : BaseController
{
    private readonly IHostService _hostService;

    public HostController(IHostService hostService)
    {
        _hostService = hostService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<HostDetailsDto>> GetHost(Guid id) => OkOrNotFound(await _hostService.GetAsync(id));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<HostDto>>> GetAllHostsAsync() => Ok(await _hostService.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult> CreateHostAsync(HostDto dto)
    {
        await _hostService.AddAsync(dto);
        return CreatedAtAction(nameof(GetHost), new
        {
            id = dto.HostId,
        }, null);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateHostAsync(Guid id, HostDetailsDto dto)
    {
        dto.HostId = id;
        await _hostService.UpdateAsync(dto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteHostAsync(Guid id)
    {
        await _hostService.DeleteAsync(id);
        return NoContent();
    }
    
}