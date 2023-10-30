namespace Shared.Models;

public class Post
{    
    public int Id { get; set; }  // for each post with title and body?
   // public User Owner { get; }  
    public string Title { get; }
    public string Postbody { get; }
    
   //add also for text on the post

   // public Post(User owner, string title)  
   public Post(string title, string postbody) 
    {  
       // Owner = owner;
        Title = title;
        Postbody = postbody;
    }
}