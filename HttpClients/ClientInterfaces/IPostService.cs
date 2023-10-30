using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
   // Task CreateAsync(PostCreationDto dto);
    Task<Post>CreateAsync(PostCreationDto dto);
    //or create?
    
    //finish WEBAPI getposts first
    Task<IEnumerable<Post>> GetPosts(string? postsContains = null);  //postContains
    //or with Async??
    
    //view 1 post
    Task<ViewAPostDto> GetByIdAsync(int id);
}