using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raimun.App.Services.Implement.Users
{
    public class UserCreateResponse
    {
        public string Username { get; set; }

        public string Message { get; set; }
    }
}
