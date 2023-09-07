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

        //IEnumerable<User> GetAllUser(bool trackChanges);
        //User GetUserById(int id, bool trackChanges);
        //void CreateUser(User user);
        //void UpdateUser(User user);
        //void DeleteUser(User user);

        [HttpGet]
        public IActionResult GetAllUser()
        {
            try
            {
                var users = _manager.UserService.GetAllUser(false);
                return Ok(users);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

