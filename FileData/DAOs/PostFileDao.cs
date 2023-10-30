using Application.DaoInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class PostFileDao: IPostDao  //changes
{
    private readonly FileContext context;

    public PostFileDao(FileContext context) //
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)//
    {
        int id = 1;
        if (context.Posts.Any()) //
        {
            id = context.Posts.Max(t => t.Id); //
            id++;
        }

        post.Id = id;//
        
        context.Posts.Add(post); //
        context.SaveChanges();

        return Task.FromResult(post);
    }

    //getpost sync??
    
    //code added for search user:
    public Task<IEnumerable<Post>> GetAsync(ViewAllPostsDto postsContains)
    {
        IEnumerable<Post> posts = context.Posts.AsEnumerable();
        if (postsContains.PostsContains != null)
        {
            posts = context.Posts.Where(p => p.Title.Contains(postsContains.PostsContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(posts);
    }
    
    //get 1 post
    public Task<Post?> GetByIdAsync(int postId)
    {
        Post? existing = context.Posts.FirstOrDefault(p => p.Id == postId);
        return Task.FromResult(existing);
    }
}