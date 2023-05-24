using BlazorMealOrdering.Server.Services.Infrastructure;
using BlazorMealOrdering.Shared.Dtos;
using BlazorMealOrdering.Shared.ResponseModels;
using BlazorMealOrdering.Shared.ResponseModels.Base;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Login")]
        public ServiceResponse<string> Login(string email, string password)
        {
            return new ServiceResponse<string>()
            {
                Value = _userService.Login(email, password)
            };
        }

        [HttpGet("GetAllUsers")]
        public async Task<ServiceResponse<List<UserDto>>> GetAllUsers()
        {
            return new ServiceResponse<List<UserDto>>()
            {
                Value = await _userService.GetAllUsers()
            };
        }

        [HttpPost("CreateUser")]
        public async Task<ServiceResponse<UserDto>> CreateUser([FromBody] UserDto userDto)
        {
            return new ServiceResponse<UserDto>()
            {
                Value = await _userService.CreateUser(userDto)
            };
        }

        [HttpPost("UpdateUser")]
        public async Task<ServiceResponse<UserDto>> UpdateUser([FromBody] UserDto User)
        {
            return new ServiceResponse<UserDto>()
            {
                Value = await _userService.UpdateUser(User)
            };
        }

        [HttpGet("GetUserById/{Id}")]
        public async Task<ServiceResponse<UserDto>> GetUserById(Guid Id)
        {
            return new ServiceResponse<UserDto>()
            {
                Value = await _userService.GetUserById(Id)
            };
        }

        [HttpPost("DeleteUserById")]
        public async Task<BaseResponse> DeleteUserById([FromBody] Guid userId)
        {
            await _userService.DeleteUserById(userId);
            return new BaseResponse();
        }
    }
}