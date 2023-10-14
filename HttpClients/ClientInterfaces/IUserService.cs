using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User>Create(UserCreationDto dto);  // works better
    //or CreateAsync 
    Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
}