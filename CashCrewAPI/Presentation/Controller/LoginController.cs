using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.DataTransferObjects;
using Presentation.ActionFilters;
using Entities.RequestFeatures;
using System.Text.Json;
using Entities.Models;

namespace Presentation.Controller
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
	[Route("api/Login")]
    public class LoginController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public LoginController(IServiceManager manager)
        {
            _manager = manager;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("SignIn")]
        public async Task<LoginResponseModel> SignIn([FromBody] LoginDto loginDto)
        {
            return await _manager.LoginService.LoginAsync(loginDto);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("SignUp")]
        public async Task<UserDto> SignUp([FromBody] UserDtoForInsertion user)
        {
            return await _manager.UserService.CreateUserAsync(user);
        }

    }
}

