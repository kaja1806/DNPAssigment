namespace Shared.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
    public int SecurityLevel { get; set; }
}

public class ListOfUsers
{
    public List<User> Users { get; set; }
}