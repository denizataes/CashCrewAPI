using System;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _manager;

        public UserManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public void CreateUser(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            _manager.User.CreateUser(user);
            _manager.Save();
        }

        public void DeleteUser(User user)
        {
            var entity = _manager.User.GetUserByIdAsync(user.ID, false);
            if(entity is null)
                throw new ArgumentNullException(nameof(user));

            _manager.User.DeleteUser(user);
            _manager.Save();
        }

        public IEnumerable<User> GetAllUser(bool trackChanges)
        {
            return _manager.User.GetAllUser(trackChanges);
        }

        public User GetUserById(int id, bool trackChanges)
        {
            return _manager.User.GetUserByIdAsync(id, trackChanges);
        }

        public void UpdateUser(User user)
        {
            //
        }
    }
}

