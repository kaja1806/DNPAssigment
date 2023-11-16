using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    // method to create a new post asynchronously
    Task<PostModel> CreateAsync(PostCreationDto dto);

    // method to retreive a list of posts asynchronously
    public Task<IEnumerable<PostModel>> GetAsync(ViewAllPostsDto postsContains);

    // method to retrieve a specific post by its id 
    Task<ViewAPostDto> GetByIdAsync(int id);
}