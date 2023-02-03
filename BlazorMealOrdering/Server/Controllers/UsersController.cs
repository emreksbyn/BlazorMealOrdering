using BlazorMealOrdering.Server.Services.Infrastructure;
using BlazorMealOrdering.Shared.Dtos;
using BlazorMealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("get-all-users")]
        public async Task<ServiceResponse<List<UserDto>>> GetAllUsers()
        {
            return new ServiceResponse<List<UserDto>>()
            {
                Value = await _userService.GetAllUsers()
            };
        }

        //[HttpPost("create-user")]
        //public async Task<ServiceResponse<UserDto>> CreateUser(UserDto userDto)
        //{
        //    return new ServiceResponse<UserDto>()
        //    {
        //        Value = await _userService.CreateUser(userDto)
        //    };
        //}
    }
}