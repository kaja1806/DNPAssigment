using Application.DaoInterfaces;
using Database.Models;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    // store the FileContext instance
    private readonly AppDbContext _context;

    public PostFileDao(AppDbContext context)
    {
        // initialize the FileContext via constructor injection
        this._context = context;
    }

    public Task<PostModel> CreateAsync(PostModel postModel)
    {
        //initialize the default ID for the new post
        int id = 1;
        if (_context.Posts.Any())
        {
            // calcualte the next available ID
            id = _context.Posts.Max(t => t.Id);
            id++;
        }

        postModel.Id = id;

        // add the post to the context
        _context.Posts.Add(postModel);
        _context.SaveChanges();

        return Task.FromResult(postModel);
    }

    public Task<IEnumerable<PostModel>> GetAsync(ViewAllPostsDto postsContains)
    {
        // get all posts from the context
        IEnumerable<PostModel> posts = _context.Posts.AsEnumerable();
        if (postsContains.PostsContains != null)
        {
            // filter posts based on the title
            posts = _context.Posts.Where(p =>
                p.Title.Contains(postsContains.PostsContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(posts);
    }

    public Task<PostModel?> GetByIdAsync(int postId)
    {
        PostModel? existing = _context.Posts.FirstOrDefault(p => p.Id == postId);
        return Task.FromResult(existing);
    }
}