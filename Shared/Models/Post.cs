namespace Shared.Models;

public class Post
{    
    public int Id { get; set; } 
    public string Title { get; }
    public string Postbody { get; }
   public Post(string title, string postbody) 
    {  
        Title = title;
        Postbody = postbody;
    }
}