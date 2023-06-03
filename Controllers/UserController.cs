using AspNetWebApiWithMongoDb.Dtos;
using AspNetWebApiWithMongoDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetWebApiWithMongoDb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync([FromQuery] UserSearchDto userSearchDto)
    {
        return Ok(await _userService.GetUsersAsync(userSearchDto));
    }
    
    [HttpGet("{id}/Address")]
    public async Task<IActionResult> GetUserAddressesAsync(string id)
    {
        return Ok(await _userService.GetUserAddressesAsync(id));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(string id)
    {
        return Ok(await _userService.GetUserByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateDto userCreateDto)
    {
        return Created("User created!", await _userService.CreateUserAsync(userCreateDto));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] UserUpdateDto userUpdateDto)
    {
        userUpdateDto.Id = id;
        await _userService.UpdateUserAsync(userUpdateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(string id)
    {
        await _userService.DeleteAsync(id);
        return Ok();
    }
}