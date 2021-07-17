using Raimun.App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raimun.App.Services.Implement.Users
{
    public class UserLoginResponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }


        public UserLoginResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
        }

        public UserLoginResponse(string message) {

            Message = message;
        }
    }
}
