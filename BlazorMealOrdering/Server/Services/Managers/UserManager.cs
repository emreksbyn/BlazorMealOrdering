using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorMealOrdering.Server.Data.Context;
using BlazorMealOrdering.Server.Data.Models;
using BlazorMealOrdering.Server.Services.Infrastructure;
using BlazorMealOrdering.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlazorMealOrdering.Server.Services.Managers
{
    public class UserManager : IUserService
    {
        private readonly IMapper _mapper;
        private readonly MealOrderinDbContext _context;
        public UserManager(IMapper mapper, MealOrderinDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _context.Users
                                  .Where(u => u.IsActive)
                                  .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync();
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            return await _context.Users
                                 .Where(u => u.Id == id)
                                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                 .FirstOrDefaultAsync();
        }
        public async Task<UserDto> CreateUser(UserDto user)
        {
            var dbUser = await _context.Users.FindAsync(user.Id);

            if (dbUser != null) throw new Exception("User already exist");

            dbUser = _mapper.Map<User>(user);

            await _context.Users.AddAsync(dbUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<UserDto> UpdateUser(UserDto user)
        {
            var dbUser = await _context.Users.FindAsync(user.Id);

            if (dbUser == null) throw new Exception("User not found");

            _mapper.Map(user, dbUser);
            //_context.Users.Update(dbUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<bool> DeleteUserById(Guid id)
        {
            var dbUser = await _context.Users.FindAsync(id);

            if (dbUser == null) throw new Exception("User not found");

            _context.Users.Remove(dbUser);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}