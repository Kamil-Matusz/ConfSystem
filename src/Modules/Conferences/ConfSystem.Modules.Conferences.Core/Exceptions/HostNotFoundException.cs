using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Conferences.Core.Exceptions;

internal class HostNotFoundException : CustomException
{
    public Guid Id { get;}
    public HostNotFoundException(Guid id) : base($"Host with ID: '{id}' was not found.")
    {
        Id = id;
    }
    
}