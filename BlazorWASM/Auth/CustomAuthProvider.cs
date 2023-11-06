using System.Security.Claims;
using BlazorWasm.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWasm.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    // private field to hold an instance of IAuthService
    private readonly IAuthService _authService;

    // constructor with IAuthService as a parameter and assigns it to the _authService field
    public CustomAuthProvider(IAuthService authService)
    {
        _authService = authService;
        // OnAuthStateChanges event to handle authentication state changes
        authService.OnAuthStateChanged += AuthStateChanged;
    }

    // to override GetAuthenticationStateAsync method to provide the current authentication state
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // retrieve ClaimsPrincipal
        ClaimsPrincipal principal = await _authService.GetAuthAsync();

        // create new AuthenticationState using retrieved ClaimsPrincipal
        return new AuthenticationState(principal);
    }

    // method to handle changes in the authentication state
    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}
