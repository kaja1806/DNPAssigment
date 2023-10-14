namespace Shared.DTOs;

public class PostCreationDto
{
     
  // public int OwnerId { get; }
    public string Title { get; } //Change later? or needed for Post title?

  //  public PostCreationDto(int ownerId, string title)
    public PostCreationDto( string title)
    {
      // OwnerId = ownerId;
        Title = title;
    }
}