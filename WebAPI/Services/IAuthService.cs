using Shared.Models;

namespace WebAPI.Services;

public interface IAuthService
{
    Task<UserModel> ValidateUser(string username, string password);
    Task RegisterUser(UserModel? userModel);
}