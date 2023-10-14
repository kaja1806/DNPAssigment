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
        /*User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }*/

        ValidatePost(dto); //
      //  Post post = new Post(user, dto.Title); //
      
      Post post = new Post(dto.Title); //
        Post created = await postDao.CreateAsync(post); //
        return created;
    }

    private void ValidatePost(PostCreationDto dto) //
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    } 
}