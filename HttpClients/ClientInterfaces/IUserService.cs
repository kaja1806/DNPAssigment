using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<UserModel> Create(UserCreationDto dto);
    Task<IEnumerable<UserModel>> GetUsers(string? usernameContains = null);
}