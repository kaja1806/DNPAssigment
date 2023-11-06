namespace Shared.DTOs;

public class PostCreationDto
{
    public string Title { get; }
    public string Postbody { get; }
    public PostCreationDto( string title, string postbody)
    {
        Title = title;
        Postbody = postbody;
    }
}