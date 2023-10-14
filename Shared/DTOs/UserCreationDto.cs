namespace Shared.DTOs;

public class UserCreationDto
{
    public string UserName { get;}
    //expand this with password later

    public UserCreationDto(string userName)  //expand here as well?
    {
        UserName = userName;
    }
}