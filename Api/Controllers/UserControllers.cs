using Core;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserControllers : ControllerBase
    {
        private readonly IUserService _userService;

        public UserControllers(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("users/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _userService.Authenticate(request.Email, request.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new {token});
        }

        [HttpPost("users")]
        [Authorize]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var createdUser = await _userService.CreateUser(user);
            return CreatedAtAction(nameof(_userService.GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpGet("usersLista")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("users/{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPut("users/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(User user)
        {


            if (!(await _userService.UpdateUser(user)))
            {
                return NotFound();
            }


            return NoContent();
        }

        [HttpDelete("users/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if(!(await _userService.DeleteUser(id)))
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
