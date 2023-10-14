namespace Shared.Models;

public class Post
{    
    public int Id { get; set; }
   // public User Owner { get; }  
    public string Title { get; }
   // public bool IsCompleted { get; } 
    
   //add also for text on the post

   // public Post(User owner, string title)  
   public Post(string title) 
    {  
       // Owner = owner;
        Title = title;
    }
}