namespace Shared.DTOs;

public class PostCreationDto
{
     
  // public int OwnerId { get; }
    public string Title { get; } //Change later? or needed for Post title?
    public string Postbody { get; }
  //  public PostCreationDto(int ownerId, string title)
    public PostCreationDto( string title, string postbody)
    {
      // OwnerId = ownerId;
        Title = title;
        Postbody = postbody;
    }
}