using burger_supply_backend.Database;
using Microsoft.EntityFrameworkCore;

namespace burger_supply_backend.Middlewares;

public class AuthMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var sessionId = context.Request.Cookies["SessionId"];
        if (sessionId == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        if (context.RequestServices.GetService(typeof(DataContext)) is not DataContext dbContext)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return;
        }
        
        var session = await dbContext.Sessions.FirstOrDefaultAsync(s => s.SessionId == sessionId);
        if (session == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }
        
        if (session.ExpiresAt < DateTime.Now)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            dbContext.Sessions.Remove(session);
            await dbContext.SaveChangesAsync();
            return;
        }
        
        await next(context);
    }
}