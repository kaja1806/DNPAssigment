namespace Shared.DTOs;

public class UserCreationDto
{
    public string UserName { get; }
    public string Password { get; }


    public UserCreationDto(string userName, string password) //expand here as well?
    {
        UserName = userName;
        Password = password;
    }
}