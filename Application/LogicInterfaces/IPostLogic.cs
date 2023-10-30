using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    public Task<IEnumerable<Post>> GetAsync(ViewAllPostsDto postsContains);
    Task<ViewAPostDto> GetByIdAsync(int id);  // title also?
}