using Shared.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    // method to create a new user asynchronously
    Task<User> CreateAsync(User user);
    
    // method to retrieve a user by their username asynchronously
    Task<User?> GetByUsernameAsync(string userName);
    
    // method to retrieve a user by their id
    Task<User?> GetByIdAsync(int id);
}