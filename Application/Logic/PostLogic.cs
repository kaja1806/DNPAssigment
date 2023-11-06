using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

//define class that implements the IPostLogic interface 
public class PostLogic : IPostLogic
{
    // private field to hold an instance of IPostDao interface
    private readonly IPostDao postDao;  

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao; 
    }
    
    // implementation of CreateAsync method defined in the IPostLogic interface
    public async Task<Post> CreateAsync(PostCreationDto dto) 
    {
      
// call a method to validate the PostCreationDto
        ValidatePost(dto); 
      
        // create a new Post object
      Post post = new Post(dto.Title, dto.Postbody); 
        Post created = await postDao.CreateAsync(post); 
      
        // return created post
        return created;
    }
    
    public Task<IEnumerable<Post>> GetAsync(ViewAllPostsDto postsContains)
    {
        // call teh GetAsync method on the IPostDao instance to retrieve posts asynchronously
        return postDao.GetAsync(postsContains);
    }
    
  
    public async Task<ViewAPostDto> GetByIdAsync(int id)  
    {
        // call the method on the IPostDao to retrieve a post by its id
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }
// create a ViewPostDto object from the retrieved Post and return it 
        return new ViewAPostDto(post.Id, post.Title, post.Postbody);
    }
    
// method to check if the title and post body are empty
    private void ValidatePost(PostCreationDto dto) //
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Postbody)) throw new Exception("Post cannot be empty.");
    } 
}