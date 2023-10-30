using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    
   // Task<User?> GetByUsernameAsync(string userName);  // get back to this later if needed
    
    //added code for search user param:
    Task<IEnumerable<Post>> GetAsync(ViewAllPostsDto postsContains);
    
    // for just 1 post
    Task<Post> GetByIdAsync(int id); // and title?

}