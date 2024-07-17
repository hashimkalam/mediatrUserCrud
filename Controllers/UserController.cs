using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]  // base endpoint
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository; // user repo instance

    // constructor
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // controller to get user by id
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // controller to get all users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();

        if (!users.Any())
        {
            return NoContent();
        }

        return Ok(users);
    }

    // controller to add a user
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var createdUser = await _userRepository.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    // controller to update a user by id passed
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, User user)
    {
        if (id != user.Id) // checks id is valid
        {
            return BadRequest("Invalid ID - use an existing id instead"); // if not returns an appropriate msg
        }

        await _userRepository.UpdateUserAsync(user);
        return Ok(user);
    }

    // controller to delete a user by id passed
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {

        var userToDelete = await _userRepository.GetUserByIdAsync(id); // Find user by id

        if (userToDelete == null) // checks id is valid
        {
            return BadRequest("Invalid ID - use an existing id instead"); // if not returns an appropriate msg
        }


        await _userRepository.DeleteUserAsync(id);

        return Ok("User deleted successfully"); // returns 200 OK
    }

}
