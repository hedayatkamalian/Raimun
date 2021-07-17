using Raimun.App.Entities;
using Raimun.App.Services.Implement.Users;
using Raimun.App.Services.Interface.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raimun.App.Services.Interface.Users
{
    public interface IUsersService
    {
        Task<User> GetByIdAsync(long id);
        Task<bool> CreateUserAsync(UserCreateRequest request);
        Task<bool> CheckUsernameExistAsync(string Username);
        Task<IList<User>> GetAllUsersAsync();
        Task<UserLoginResponse> AuthenticateAsync(UserLoginRequest request);
    }
}
