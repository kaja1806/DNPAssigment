using System.ComponentModel.DataAnnotations;
using Application.Helpers;
using Database.Models;
using Shared.Models;

namespace WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private UserModel? _users;
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public Task<UserModel> ValidateUser(string username, string password)
        {
            _users = _context.Users.FirstOrDefault(u => u.Username == username);

            if (_users == null)
            {
                throw new Exception("User not found");
            }

            if (_users.Token != null)
            {
                // Clear existing token if it exists
                _users.Token = null;
                _context.SaveChanges();
            }

            var hashedPassword = Hashing.HashString(password);

            if (!_users.Password.Equals(hashedPassword))
            {
                throw new Exception("Password mismatch");
            }

            return Task.FromResult(_users);
        }

        public Task<UserModel> GetUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(UserModel? userModel)
        {
            if (string.IsNullOrEmpty(userModel?.Username))
            {
                throw new ValidationException("Username cannot be null");
            }

            if (string.IsNullOrEmpty(userModel.Password))
            {
                throw new ValidationException("Password cannot be null");
            }

            _users = userModel;

            return Task.CompletedTask;
        }
    }
}