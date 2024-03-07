using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Volunteer.BL.Interfaces;

namespace Volunteer.WebAPI.Controllers
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

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var checkUser = await _userService.GetUserByEmail(userDTO.Email);
            if (checkUser != null)
            {
                return BadRequest("User is already exist");
            }

            if (await _userService.AddUser(userDTO))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User is not added");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var userToUpdate = await _userService.GetUserByEmail(userDTO.Email);
            if (userToUpdate == null)
            {
                return NotFound("User not found");
            }
            if (await _userService.UpdateUser(userDTO))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update user");
            }
        }

        [HttpDelete("{userId:Guid}")]   
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            if(userId == Guid.Empty)
            {
                return BadRequest("Id does not exist");
            }
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound("No user found");
            }
            if (await _userService.DeleteUserById(userId))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User is not deleted");
            }
        }

        [HttpGet("{userId:Guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("Id does not exist");
            }
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound("No user found");
            }
 
            return Ok(user);
        } 
    }
}

