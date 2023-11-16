using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient _client;

    public PostHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<PostModel> CreateAsync(PostCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/posts", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        PostModel postModel = JsonSerializer.Deserialize<PostModel>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return postModel;
    }

    public async Task<IEnumerable<PostModel>> GetPosts(string? postsContains = null)

    {
        string uri = "/posts";
        if (!string.IsNullOrEmpty(postsContains))
        {
            uri += $"?title={postsContains}";
        }

        HttpResponseMessage response = await _client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Console.WriteLine(result);
        IEnumerable<PostModel> posts = JsonSerializer.Deserialize<IEnumerable<PostModel>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return posts; // or titles?
    }

    public async Task<ViewAPostDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"/posts/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ViewAPostDto post = JsonSerializer.Deserialize<ViewAPostDto>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return post;
    }
}