using Microsoft.AspNetCore.Mvc;
using Database.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogoutController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Logout(string token)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Token == token);

                if (user == null)
                {
                    return new NotFoundObjectResult("User not found");
                }

                if (user.Token == null || user.Token != token)
                {
                    return new OkObjectResult("Not logged in");
                }

                // Clear the user's token to log them out
                user.Token = null;
                _context.SaveChanges();

                return new OkObjectResult("Logged out");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e);
            }
        }
    }
}