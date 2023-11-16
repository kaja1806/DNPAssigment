using Application.DaoInterfaces;
using Application.Helpers;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

// public class which implements the IUserLogic interface
public class UserLogic : IUserLogic
{
    // private field to hold an instance of IUserDao
    private readonly IUserDao _userDao;

    // constructor for UserLogic
    public UserLogic(IUserDao userDao)
    {
        this._userDao = userDao;
    }

    // method to create a user based on the provided UserCreationDto
    public async Task<UserModel?> CreateUser(UserCreationDto dto)
    {
        // check if a user with given username alsready exists
        var existing = _userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        // create new User instance with data from UserCreationDto
        var toCreate = new UserModel
        (
            dto.UserName,
            Hashing.HashString(dto.Password),
            dto.Role
        );

// create the user asynchronously and store the result
        var created = await _userDao.CreateAsync(toCreate);

        return created;
    }

// private class to validate the data in the UserCreationDto
    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;
// check if username lenght is less than 3 characters
        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");
// check if username is greather than 15 chracters
        if (userName.Length > 15)
            // throw exception if it is too long
            throw new Exception("Username must be less than 16 characters!");
    }
}