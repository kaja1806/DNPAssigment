/*using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Json;
using Shared.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IList<User> _users;

    public AuthService()
    {
        string usersJson = File.ReadAllText("data.json");
        _users = JsonSerializer.Deserialize<List<User>>(usersJson);
    }


    public Task<User> ValidateUser(string username, string password)
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

        _users.Add(user);

        return Task.CompletedTask;
    }
}*/


using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Shared.Models;

namespace WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IList<User>? _users;

        public AuthService(IList<User> users)
        {
            var usersJson = File.ReadAllText("data.json");
            _users = JsonSerializer.Deserialize<List<User>>(usersJson);
        }

        public Task<User> ValidateUser(string username, string password)
        {
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
