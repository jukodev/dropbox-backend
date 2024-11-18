using burger_supply_backend.Database;
using Microsoft.AspNetCore.Mvc;

namespace burger_supply_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(DataContext dataContext): ControllerBase
{
    [HttpPost("/login")]
    public async Task<IActionResult> Login()
    {
        return Unauthorized();
    }
    
}