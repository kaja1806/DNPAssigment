using System.Security.Claims;
using Shared.Models;

namespace BlazorWasm.Services;

public interface IAuthService
{
    // asynchronous method for user login
    public Task LoginAsync(string username, string password);

    // asynchronous method for user logout
    public Task LogoutAsync();

    // asynchronous method for user registration
    public Task RegisterAsync(UserModel userModel);

    // asynchronous method to get authentication data
    public Task<ClaimsPrincipal> GetAuthAsync();

// event handler for authentication state changes
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}