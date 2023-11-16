using Database.Models;
using Shared.Models;

namespace Database.Controllers;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        UserModel?[] users = new UserModel[]
        {
            new UserModel
            (
                "Pass",
                "Weronika",
                "User"
            )
        };
        context.Users.AddRange(users);
        context.SaveChanges();
    }
}