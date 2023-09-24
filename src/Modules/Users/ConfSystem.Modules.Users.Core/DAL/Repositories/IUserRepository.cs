using ConfSystem.Modules.Users.Core.Entities;

namespace ConfSystem.Modules.Users.Core.DAL.Repositories;

internal interface IUserRepository
{
    Task<User> GetUserAsync(Guid id);
    Task<User> GetUserAsync(string email);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
}