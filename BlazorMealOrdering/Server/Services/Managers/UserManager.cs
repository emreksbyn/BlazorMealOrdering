using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorMealOrdering.Server.Data.Context;
using BlazorMealOrdering.Server.Data.Models;
using BlazorMealOrdering.Server.Services.Infrastructure;
using BlazorMealOrdering.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorMealOrdering.Server.Services.Managers
{
    public class UserManager : IUserService
    {
        private readonly IMapper _mapper;
        private readonly MealOrderinDbContext _context;
        private readonly IConfiguration _configuration;
        public UserManager(IMapper mapper, MealOrderinDbContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _context.Users
                                  //.Where(u => u.IsActive)
                                  .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                  .OrderBy(u => u.CreateDate)
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

        public Task<string> Login(string email, string password)
        {
            // db kullanici dogrulamasi.

            // token olusturma
            string value = _configuration["Jwt:SecurityKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(value));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(int.Parse(_configuration["Jwt:ExpiryInDays"].ToString()));

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };

            string issuer = _configuration["Jwt:Issuer"];
            string audience = _configuration["Jwt:Audience"];

            var token = new JwtSecurityToken(issuer, audience, claims, null, expiry, credential);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}