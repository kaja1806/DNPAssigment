namespace Shared.DTOs;

public class ViewAPostDto
{
    
    public int Id { get; }
   public string Title { get; }
   public string Postbody { get; }

   public ViewAPostDto(int id, string title, string postbody)
    {
        Id = id;
      
        Title = title;

        Postbody = postbody;

    }
}