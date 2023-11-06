using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Shared.DTOs;
using Shared.Models;

namespace BlazorWasm.Services.Http;

// JwtAuthService class that implements IAuthService
public class JwtAuthService : IAuthService
{
    // initialize an HttpClient instance
    private readonly HttpClient _client = new();

    // private variable for simple caching
    public static string? Jwt { get; private set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

    // asynchronous method for user login
    public async Task LoginAsync(string username, string password)
    {
        // create UserLoginDto instance
        UserLoginDto userLoginDto = new()
        {
            Username = username,
            Password = password
        };
         // serialize UserLoginDto to JSON
         string userAsJson = JsonSerializer.Serialize(userLoginDto);
        // create HTTP content
         StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        // send a POST request
        HttpResponseMessage response = await _client.PostAsync("https://localhost:7015/auth/login", content);
        // read response content
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            // exception for unsuccessful responses
            throw new Exception(responseContent);
        }

        string token = responseContent;
        Jwt = token;
// create ClaimsPrincipal
        ClaimsPrincipal principal = CreateClaimsPrincipal();
// invoke authentication state change callback
        OnAuthStateChanged.Invoke(principal);
    }
// create ClaimsPrincipal based on JWT
    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);

        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }

    // asynchronous method for user logout
    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }

    // asynchronous method for user registration
    public async Task RegisterAsync(User user)
    {
        string userAsJson = JsonSerializer.Serialize(user);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        // send a POST request
        HttpResponseMessage response = await _client.PostAsync("https://localhost:7015/auth/register", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            // exception for unsuccessful responses
            throw new Exception(responseContent);
        }
    }
       // asyncrhonous method to get authentication data
    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        // create a ClaimsPrincipal
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }


    //  methods  from https://github.com/SteveSandersonMS/presentation-2019-06-NDCOslo/blob/master/demos/MissionControl/MissionControl.Client/Util/ServiceExtensions.cs
   // methods to parse and decode claims from JWT
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}