using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<Post>CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> GetPosts(string? postsContains = null); 
    Task<ViewAPostDto> GetByIdAsync(int id);
}