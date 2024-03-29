using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService

{
    private readonly HttpClient _client;

    public UserHttpClient(HttpClient client)
    {
        _client = client;
    }


    public async Task<UserModel> Create(UserCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/users", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        UserModel userModel = JsonSerializer.Deserialize<UserModel>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return userModel;
    }

    public Task<IEnumerable<UserModel>> GetUsers(string? usernameContains = null)
    {
        throw new NotImplementedException();
    }
}