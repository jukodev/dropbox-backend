using burger_supply_backend.Database;
using burger_supply_backend.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace burger_supply_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(DataContext dataContext): ControllerBase
{
    public class LoginRequest
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
    
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest data)
    {
        var user = await dataContext.Users.Where(u => u.Username == data.Username && u.Password == data.Password).FirstOrDefaultAsync();
        if (user == null)
        {
            return Unauthorized();
        }
        
        var session = new SessionDbModel
        {
            SessionId = Guid.NewGuid().ToString(),
            User = user,
            ExpiresAt = DateTime.Now.AddDays(31),
            CreatedAt = DateTime.Now
        };
        await dataContext.Sessions.AddAsync(session);
        await dataContext.SaveChangesAsync();
        
        Response.Cookies.Append("SessionId", session.SessionId, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true
        });
        
        return Ok();
    }
    
    [HttpPost("/logout")]
    public async Task<IActionResult> Logout()
    {
        var sessionId = Request.Cookies["SessionId"];
        if (sessionId == null)
        {
            return Unauthorized();
        }
        
        var session = await dataContext.Sessions.FirstOrDefaultAsync(s => s.SessionId == sessionId);
        if (session == null)
        {
            return Unauthorized();
        }
        
        dataContext.Sessions.Remove(session);
        await dataContext.SaveChangesAsync();
        
        Response.Cookies.Delete("SessionId");
        
        return Ok();
    }
    
    
}