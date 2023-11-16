using Shared.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    // method to create a new user asynchronously
    Task<UserModel> CreateAsync(UserModel? user);

    // method to retrieve a user by their username asynchronously
    UserModel? GetByUsernameAsync(string userName);

    // method to retrieve a user by their id
    Task GetByIdAsync(int id);
}