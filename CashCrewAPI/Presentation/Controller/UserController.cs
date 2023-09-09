using System;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
namespace Presentation.Controller
{
	[Controller]
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
    }
}

