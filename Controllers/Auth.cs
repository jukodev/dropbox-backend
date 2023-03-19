using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace drop_grungus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        [HttpPost("/login")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Login([FromBody] string username, [FromBody] string password) {
            return NotFound();
        }

        [HttpPost("/validate")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Login([FromBody] string token)
        {
            return NotFound();
        }
    }
}
