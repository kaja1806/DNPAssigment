using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Shared.Models;

namespace WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private List<User>? _users;
        private List<ListOfUsers>? _listOfUsers;

        public AuthService(List<User>? users)
        {
            _users = users;
        }

        public Task<User> ValidateUser(string username, string password)
        {
            var usersJson = File.ReadAllText("data.json");
            var listOfUsers = JsonConvert.DeserializeObject<ListOfUsers>(usersJson);
            _users = listOfUsers?.Users;

            if (_users != null)
            {
                User? existingUser = _users.FirstOrDefault(u =>
                    u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (existingUser == null)
                {
                    throw new Exception("User not found");
                }

                if (!existingUser.Password.Equals(password))
                {
                    throw new Exception("Password mismatch");
                }

                return Task.FromResult(existingUser);
            }

            return null;
        }

        public Task<User> GetUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                throw new ValidationException("Username cannot be null");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ValidationException("Password cannot be null");
            }

            _users?.Add(user);

            return Task.CompletedTask;
        }
    }
}