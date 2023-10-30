using Database.Models;

namespace Database.Controllers;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        var users = new UserModel[]
        {
            new UserModel
            {
                Password = "Pass",
                Username = "Weronika"
            }
        };
        context.Users.AddRange(users);
        context.SaveChanges();
    }
}