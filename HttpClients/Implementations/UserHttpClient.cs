using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService

{
    private readonly HttpClient client;

    public UserHttpClient(HttpClient client) 
    {
        this.client = client;
    }

    
    public async Task<User> Create(UserCreationDto dto) // async method
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users", dto); // here is where client makes the POST; create user
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public Task<IEnumerable<User>> GetUsers(string? usernameContains = null)
    {
        throw new NotImplementedException();
    }
}