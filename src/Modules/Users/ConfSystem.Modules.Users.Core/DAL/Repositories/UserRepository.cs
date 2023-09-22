using ConfSystem.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Users.Core.DAL.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UsersDbContext _usersDbContext;
    private readonly DbSet<User> _users;

    public UserRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
        _users = _usersDbContext.Users;
    }

    public async Task<User> GetUserAsync(Guid id) 
        => await _users.SingleOrDefaultAsync(x => x.UserId == id);

    public async Task<User> GetUserAsync(string email) 
        => await _users.SingleOrDefaultAsync(x => x.Email == email);

    public async Task AddUserAsync(User user)
    {
        await _users.AddAsync(user);
        await _usersDbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _users.Update(user);
        await _usersDbContext.SaveChangesAsync();
    }
}