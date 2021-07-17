using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Raimun.App.Entities;
using Raimun.App.Services.Implement.Users;
using Raimun.App.Services.Implement.Weathers;
using Raimun.App.Services.Interface.Auth;
using Raimun.App.Services.Interface.Queues;
using Raimun.App.Services.Interface.Users;
using Raimun.App.Services.Interface.Weathers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raimun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IUsersService _usersService;
        private readonly IQueuesService _queuesService;
        private readonly IWeathersService _weatherService;

        public AuthController
            (IConfiguration config,
            IWeathersService weatherService,
            IUsersService usersService,
            IQueuesService queuesService
            )
        {
            _config = config;
            _usersService = usersService;
            _queuesService = queuesService;
            _weatherService = weatherService;
        }


        [AllowAnonymous]
        [Route("gettoken")]
        [HttpPost]
        public async Task<ActionResult<UserLoginResponse>> GetToken(UserLoginRequest request)
        {
            var result = await _usersService.AuthenticateAsync(request);

            return result;
        }

        [AllowAnonymous]
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<UserCreateResponse>> CreateUser(UserCreateRequest request)
        {
            var returnValue = new UserCreateResponse
            {
                Username = request.Username,
            };

            var exist = await _usersService.CheckUsernameExistAsync(request.Username);
            if (exist)
            {
                return StatusCode(500, "user exist currently ");
            }

            var result = await _usersService.CreateUserAsync(request);

            if (result)
            {
                returnValue.Message = "created";
                return Ok(returnValue);
            }
            else
            {
                return StatusCode(500, "error on creating user");
            }
        }
    }
}
