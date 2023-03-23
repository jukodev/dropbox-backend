using drop_grungus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace drop_grungus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private User[] validUsers = { new User("valid", "valid", "vtoken") };

        [HttpPost("/login")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Login([FromBody] User user) {
            if(user == null)
            {
                return BadRequest();
            }
            
            var _user = validUsers.FirstOrDefault(u => u.Name.Equals(user.Name) && u.Password.Equals(user.Password));
            // TODO check for valid login
            if(_user == default) {
                return Unauthorized();
            }

            return NoContent();


        }

        [HttpPost("/validate")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Validate([FromBody] string token)
        {
            return NotFound();
        }
    }
}
