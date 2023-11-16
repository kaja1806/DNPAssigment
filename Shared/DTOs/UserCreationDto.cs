namespace Shared.DTOs;

public class UserCreationDto
{
    public string UserName { get; }
    public string Password { get; }
    public string Role { get; set; } = "User";


    public UserCreationDto(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}