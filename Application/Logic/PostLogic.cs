using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

//define class that implements the IPostLogic interface 
public class PostLogic : IPostLogic
{
    // private field to hold an instance of IPostDao interface
    private readonly IPostDao _postDao;

    public PostLogic(IPostDao postDao)
    {
        _postDao = postDao;
    }

    // implementation of CreateAsync method defined in the IPostLogic interface
    public async Task<PostModel> CreateAsync(PostCreationDto dto)
    {
// call a method to validate the PostCreationDto
        ValidatePost(dto);

        // create a new Post object
        PostModel postModel = new PostModel(dto.Title, dto.Postbody);
        PostModel created = await _postDao.CreateAsync(postModel);

        // return created post
        return created;
    }

    public Task<IEnumerable<PostModel>> GetAsync(ViewAllPostsDto postsContains)
    {
        // call teh GetAsync method on the IPostDao instance to retrieve posts asynchronously
        return _postDao.GetAsync(postsContains);
    }


    public async Task<ViewAPostDto> GetByIdAsync(int id)
    {
        // call the method on the IPostDao to retrieve a post by its id
        PostModel? post = await _postDao.GetByIdAsync(id);
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