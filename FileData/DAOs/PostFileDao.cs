using Application.DaoInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class PostFileDao: IPostDao  
{
    // store the FileContext instance
    private readonly FileContext context;

    public PostFileDao(FileContext context) 
    {
        // initialize the FileContext via constructor injection
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        //initialize the default ID for the new post
        int id = 1;
        if (context.Posts.Any()) 
        {
            // calcualte the next available ID
            id = context.Posts.Max(t => t.Id); 
            id++;
        }

        post.Id = id;
        
        // add the post to the context
        context.Posts.Add(post); 
        context.SaveChanges();

        return Task.FromResult(post);
    }
    
    public Task<IEnumerable<Post>> GetAsync(ViewAllPostsDto postsContains)
    {
        // get all posts from the context
        IEnumerable<Post> posts = context.Posts.AsEnumerable();
        if (postsContains.PostsContains != null)
        {
            // filter posts based on the title
            posts = context.Posts.Where(p => p.Title.Contains(postsContains.PostsContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(posts);
    }
    
    public Task<Post?> GetByIdAsync(int postId)
    {
        Post? existing = context.Posts.FirstOrDefault(p => p.Id == postId);
        return Task.FromResult(existing);
    }
}