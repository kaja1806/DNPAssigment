namespace Shared.DTOs;

public class ViewAllPostsDto
{
    public string? PostsContains { get;  }

    public ViewAllPostsDto(string? postsContains)
    {
        PostsContains = postsContains;
    }  
}

