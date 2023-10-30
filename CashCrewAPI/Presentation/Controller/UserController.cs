using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.DataTransferObjects;
using Presentation.ActionFilters;
using Entities.RequestFeatures;
using System.Text.Json;

namespace Presentation.Controller
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
	[Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUser([FromQuery] UserParameters userParameters)
        {
            var pagedResult = await _manager
                .UserService
                .GetAllUserAsync(userParameters,false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var users = await _manager.UserService.GetUserByIdAsync(id, false);
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername([FromRoute(Name = "username")] string username)
        {
            var users = await _manager.UserService.GetUserByUsernameAsync(username, false);
            return Ok(users);
        }


        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute(Name = "id")] int id,
            [FromBody] UserDtoForUpdate userDto)
        {
            await _manager.UserService.UpdateUserAsync(id, userDto, false);
            return NoContent(); // 204
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDtoForInsertion userDto)
        {
            var user = await _manager.UserService.CreateUserAsync(userDto);
            return StatusCode(201, user); // CreatedAtRoute()
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute(Name = "id")] int id)
        {
            await _manager.UserService.DeleteUserAsync(id);
            return Ok();
        }

    }
}

