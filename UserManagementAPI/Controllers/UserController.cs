using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.DTO;
using UserManagementAPI.Services.UserService;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Bad Request: {ex.Message}");
            }
        }

        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<User>> GetSingleUser(int id)
        {
            try
            {
                var user = await _userService.GetSingleUser(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Bad Request: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<List<User>>> AddUser(UserDto newUser )
        {
            try
            {
                var result = await _userService.AddUser(newUser);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Bad Request: {ex.Message}");
            }
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, UserDto updatedUser)
        {
            try
            {
                var result = await _userService.UpdateUser(id, updatedUser);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Bad Request: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Bad Request: {ex.Message}");
            }
        }
    }

}

