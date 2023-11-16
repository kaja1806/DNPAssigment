using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    // define a method that allows creating a new post asynchronously
    Task<PostModel> CreateAsync(PostModel postModel);

    // define a method that allows retrieving a list of posts asychronously
    Task<IEnumerable<PostModel>> GetAsync(ViewAllPostsDto postsContains);

    // define a method that allows retreiving a specific post based on id
    Task<PostModel?> GetByIdAsync(int id); // and title?
}