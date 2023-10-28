namespace ConfSystem.Shared.Infrastructure.PostgreSQL;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}