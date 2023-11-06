using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    // method to create a new post asynchronously
    Task<Post> CreateAsync(PostCreationDto dto);
    // method to retreive a list of posts asynchronously
    public Task<IEnumerable<Post>> GetAsync(ViewAllPostsDto postsContains);
    // method to retrieve a specific post by its id 
    Task<ViewAPostDto> GetByIdAsync(int id); 
}