using ConfSystem.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Shared.Infrastructure.PostgreSQL.Decorators;

[Decorator]
internal class TransactionalCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly UnitOfWorkTypeRegistry _unitOfWorkTypeRegistry;
    private readonly IServiceProvider _serviceProvider;

    public TransactionalCommandHandlerDecorator(ICommandHandler<T> handler, UnitOfWorkTypeRegistry unitOfWorkTypeRegistry, IServiceProvider serviceProvider)
    {
        _handler = handler;
        _unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
        _serviceProvider = serviceProvider;
    }
    
    public async Task HandleAsync(T command)
    {
        var unitOfWorkType = _unitOfWorkTypeRegistry.Resolve<T>();
        if (unitOfWorkType is null)
        {
            await _handler.HandleAsync(command);
            return;
        }
        
        var unitOfWork = (IUnitOfWork) _serviceProvider.GetRequiredService(unitOfWorkType);
        await unitOfWork.ExecuteAsync(() => _handler.HandleAsync(command));
    }
}