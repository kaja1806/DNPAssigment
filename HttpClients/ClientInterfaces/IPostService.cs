using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<PostModel> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<PostModel>> GetPosts(string? postsContains = null);
    Task<ViewAPostDto> GetByIdAsync(int id);
}