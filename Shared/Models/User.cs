namespace Shared.Models;

public class User
{
    public int Id { get; set; } // unique proprty but is just here for changing username in the future
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int SecurityLevel { get; set; }
}