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
        public IActionResult GetAllUser()
        {
            var users = _manager.UserService.GetAllUser(false);
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var users = _manager.UserService.GetUserById(id, false);
            return Ok(users);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateUser([FromRoute(Name = "id")] int id,
            [FromBody] UserDtoForUpdate userDto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _manager.UserService.UpdateUser(id, userDto, false);
            return NoContent(); // 204
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDtoForInsertion userDto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var user = _manager.UserService.CreateUser(userDto);
            return StatusCode(201, user); // CreatedAtRoute()
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser([FromRoute(Name = "id")] int id)
        {
            _manager.UserService.DeleteUser(id);
            return Ok();
        }

    }
}

