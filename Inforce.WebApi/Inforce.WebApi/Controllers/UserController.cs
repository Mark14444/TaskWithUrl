using Inforce.Service.Dto.AuthorizationDtos;
using Inforce.Service.Dto.UserDtos;
using Inforce.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inforce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpGet("get-me"), Authorize]
        public async Task<IActionResult> GetMe()
        {
            var userName = await _service.GetMeAsync();
            return Ok(userName);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegiserUser(CreateUserDto user)
        {
            return await _service.AddAsync(user) ? Ok() : BadRequest();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequestDto user)
        {
            var response = await _service.LoginAsync(user);
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(response);
        }
        [HttpGet("get-all"), Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("get-by-id"), Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            return Ok(user);
        }
        [HttpDelete("delete"), Authorize]
        public async Task<IActionResult> RemoveUser(int id)
        {
            return await _service.RemoveAsync(id) ? Ok() : BadRequest();
        }
        [HttpPut("update"), Authorize]
        public async Task<IActionResult> UpdateUser(UserDto entity)
        {
            return await _service.UpdateAsync(entity) ? Ok() : BadRequest();
        }
    }
}
