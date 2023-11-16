using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    // method to create a new user asynchronously
    Task<UserModel?> CreateUser(UserCreationDto userToCreate);
}