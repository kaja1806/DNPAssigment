namespace Shared.Models;

public class PostModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Postbody { get; set; }

    public PostModel(string title, string postbody)
    {
        Title = title;
        Postbody = postbody;
    }
}