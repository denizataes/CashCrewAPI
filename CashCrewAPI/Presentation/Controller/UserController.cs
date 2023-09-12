using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.DataTransferObjects;

namespace Presentation.Controller
{
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
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _manager.UserService.GetAllUserAsync(false);
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var users = await _manager.UserService.GetUserByIdAsync(id, false);
            return Ok(users);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute(Name = "id")] int id,
            [FromBody] UserDtoForUpdate userDto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            await _manager.UserService.UpdateUserAsync(id, userDto, false);
            return NoContent(); // 204
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDtoForInsertion userDto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var user = await _manager.UserService.CreateUserAsync(userDto);
            return StatusCode(201, user); // CreatedAtRoute()
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute(Name = "id")] int id)
        {
            await _manager.UserService.DeleteUserAsync(id);
            return Ok();
        }

    }
}

