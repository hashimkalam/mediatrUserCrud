using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    public readonly List<User> _users = new List<User>(); // initialising list 
 
    public Task<User> GetUserByIdAsync(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user); // return user wrapped in a completed task
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return Task.FromResult<IEnumerable<User>>(_users); // return all users as enumerable
    }

    public Task<User> CreateUserAsync(User user)
    {
        user.Id = Guid.NewGuid(); // generating an unique id
        _users.Add(user); // add user to the list
        return Task.FromResult(user); // return the created user wrapped in a completed task
    }

    public Task UpdateUserAsync(User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id); // find user by id

        if (existingUser != null)
        {
            existingUser.Name = user.Name; // update user properties
            existingUser.Age = user.Age;
        }

        return Task.CompletedTask; // Return a completed task
    }

    public Task DeleteUserAsync(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id); // Find user by id

        if (user != null)
        {
            _users.Remove(user); // Remove user from the list
        }

        return Task.CompletedTask; // Return a completed task
    }
}
