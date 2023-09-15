using ConfSystem.Shared.Abstractions;

namespace ConfSystem.Shared.Infrastructure.Services;

internal class Clock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}