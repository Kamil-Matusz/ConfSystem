using ConfSystem.Shared.Abstractions.Contexts;

namespace ConfSystem.Shared.Infrastructure.Contexts;

internal interface IContextFactory
{
    IContext Create();
}