using Application.DaoInterfaces;
using Database.Models;
using Shared.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly AppDbContext _context;

    public UserFileDao(AppDbContext context)
    {
        this._context = context;
    }

    public Task<UserModel> CreateAsync(UserModel? user)
    {
        int userId = 1;
        if (_context.Users.Any())
        {
            userId = _context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        _context.Users.Add(user);
        _context.SaveChanges();

        return Task.FromResult(user);
    }

    public UserModel? GetByUsernameAsync(string userName)
    {
        var existing = _context.Users.FirstOrDefault(u => u.Username == userName);

        return existing;
    }

    public Task GetByIdAsync(int id)
    {
        UserModel? existing = _context.Users.FirstOrDefault(u =>
            u.Id == id
        );
        return Task.FromResult(existing);
    }
}