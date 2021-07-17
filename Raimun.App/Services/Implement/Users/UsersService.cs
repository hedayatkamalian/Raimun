using Raimun.App.Services.Interface.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raimun.App.Entities;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Raimun.App.Options;
using Raimun.App.Entities.Data;
using Microsoft.EntityFrameworkCore;

namespace Raimun.App.Services.Implement.Users
{
    public class UsersService : IUsersService
    { 
        private readonly Jwt _jwt;
        private readonly ApplicationDbContext _context;

        public UsersService(IOptions<Jwt> jwt , ApplicationDbContext context)
        {
            _jwt = jwt.Value;
            _context = context;
        }

        public async Task<UserLoginResponse> AuthenticateAsync(UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync
                (x => x.Username == request.Username);

            // return null if user not found
            if (user == null)
            {
                return new UserLoginResponse("bad user or password");
            }

            var verified = BCrypt.Net.BCrypt.Verify( request.Password , user.Password);

            if (!verified)
            {
                return new UserLoginResponse("bad user or password");
            }


            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new UserLoginResponse(user, token);
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> CreateUserAsync(UserCreateRequest request)
        {

            var generator = new IdGen.IdGenerator(0);

            var user = new User
            {
                Id = generator.CreateId(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)

            };

            await _context.Users.AddAsync(user);

            var result = await _context.SaveChangesAsync();

            if (result == 1)
            {
                return true;
            }
            return false;
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwt.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> CheckUsernameExistAsync(string username)
        {
            return await _context.Users.AnyAsync(p => p.Username == username);
        }
    }
}
