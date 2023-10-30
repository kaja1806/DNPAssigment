using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;  //changes for post
   // private readonly IUserDao userDao; //

    public PostLogic(IPostDao postDao, IUserDao userDao) //
    {
        this.postDao = postDao; //
      //  this.userDao = userDao;
    }
    
    
    public async Task<Post> CreateAsync(PostCreationDto dto) //
    {
      

        ValidatePost(dto); //
      //  Post post = new Post(user, dto.Title); //
      
      Post post = new Post(dto.Title, dto.Postbody); //
        Post created = await postDao.CreateAsync(post); //
        return created;
    }
    
    public Task<IEnumerable<Post>> GetAsync(ViewAllPostsDto postsContains)
    {
        return postDao.GetAsync(postsContains);
    }
    
    //view 1 post
    public async Task<ViewAPostDto> GetByIdAsync(int id)  // tile?
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return new ViewAPostDto(post.Id, post.Title, post.Postbody);
    }
    

    private void ValidatePost(PostCreationDto dto) //
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Postbody)) throw new Exception("Post cannot be empty.");
        // other validation stuff
    } 
    
   
}