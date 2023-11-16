namespace Shared.Models;

public class UserModel
{
    public UserModel(string username, string password, string role)
    {
        Username = username;
        Password = password;
        Role = role;
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int SecurityLevel { get; set; }
    public string? Token { get; set; }
}