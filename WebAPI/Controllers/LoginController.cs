﻿using System.Globalization;
using Database.Models;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly AppDbContext _context;

    public LoginController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("check-email")]
    public IActionResult CheckEmail([FromQuery] string username)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return StatusCode(401, new { error = "This email does not have an account" });
            }

            return Ok(new { hasLogin = !string.IsNullOrEmpty(user.Password) });
        }
        catch (Exception e)
        {
            //return StatusCode(500, new { error = "An error occurred" });
            return StatusCode(500, e.StackTrace);
        }
    }

    [HttpPost("create-user")]
    public IActionResult CreateUser()
    {
        return null;
    }

    [HttpPut("create-password")]
    public IActionResult CreatePassword([FromQuery] string username, string password, string repeatedPassword)
    {
        try
        {
            if (password != repeatedPassword || password.Length <= 6 || repeatedPassword.Length <= 6)
            {
                return BadRequest(new { error = "Invalid password" });
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            user.Password = Hashing.HashString(password);
            _context.SaveChanges(); // Save changes to the database

            return Ok(new { success = "Password created" });
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error: " + e.Message);
        }
    }

    [HttpGet("login")]
    public IActionResult Login([FromQuery] string username, string password)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            if (user.Token != null)
            {
                // Clear existing token if it exists
                user.Token = null;
                _context.SaveChanges();
            }

            var hashedPassword = Hashing.HashString(password);

            if (user.Password != hashedPassword)
            {
                return Ok(new { error = "Password incorrect" });
            }

            var token = Hashing.HashString(username + DateTime.Now.ToString(CultureInfo.InvariantCulture));

            user.Token = token;
            _context.SaveChanges();

            return Ok(new { token });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }
    }


    [HttpDelete("logout")]
    public IActionResult Logout([FromQuery] int userId, string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest(new { error = "Internal error" });
        }

        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            if (user.Token == null || user.Token != token)
            {
                return Ok(new { error = "Not logged in" });
            }

            // Clear the user's token to log them out
            user.Token = null;
            _context.SaveChanges();

            return Ok(new { success = "Logged out" });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }
    }
}