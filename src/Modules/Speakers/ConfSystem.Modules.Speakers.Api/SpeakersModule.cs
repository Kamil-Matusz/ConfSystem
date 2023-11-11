using ConfSystem.Modules.Speakers.Core.DTO;
using ConfSystem.Modules.Speakers.Core.Services;
using ConfSystem.Shared.Abstractions.Modules;
using ConfSystem.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Modules.Speakers.Api;

internal class SpeakersModule : IModule
{
    public const string BasePath = "speakers-module";        
    public string Name { get; } = "Speakers";
    public string Path => BasePath;

    public IEnumerable<string> Policies { get; } = new[] {"speakers"};

    public void Use(IApplicationBuilder app)
    {
        app
            .UseModuleRequests()
            .Subscribe<SpeakerDto, object>("speakers/create", async (dto, sp) =>
            {
                var service = sp.GetRequiredService<ISpeakersService>();
                await service.CreateSpeakerAsync(dto);
                return null;
            });
    }
    
}