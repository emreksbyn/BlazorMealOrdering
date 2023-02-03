using BlazorMealOrdering.Shared.Dtos;

namespace BlazorMealOrdering.Server.Services.Infrastructure
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(Guid id);
        Task<UserDto> CreateUser(UserDto user);
        Task<UserDto> UpdateUser(UserDto user);
        Task<bool> DeleteUserById(Guid id);
    }
}