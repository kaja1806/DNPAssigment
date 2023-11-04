using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Shared.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private const string UsersFilePath = "users.json"; // Path to the JSON file storing user data

        // Logout endpoint
        [HttpPost]
        public IActionResult Logout(string token)
        {
            // Implement logout logic here (e.g., clearing the token)
            try
            {
                List<User> users = LoadUsersFromJson();
                // Your logout logic goes here
                // For example, you can clear the user's token
                // user.Token = null;
                // SaveUsersToJson(users);

                return Ok(new { success = "Logged out" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        // Load user data from the JSON file
        private List<User> LoadUsersFromJson()
        {
            if (System.IO.File.Exists(UsersFilePath))
            {
                var json = System.IO.File.ReadAllText(UsersFilePath);
                return JsonSerializer.Deserialize<List<User>>(json);
            }

            return new List<User>();
        }

        // Save user data to the JSON file
        private void SaveUsersToJson(List<User> users)
        {
            var json = JsonSerializer.Serialize(users);
            System.IO.File.WriteAllText(UsersFilePath, json);
        }
    }
}