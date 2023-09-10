using System;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public UserManager(IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
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
            if (entity is null)
            {
                _logger.LogInfo("User does not exists.");
                
                throw new ArgumentNullException(nameof(user));
            }

            _manager.User.DeleteUser(user);
            _manager.Save();
        }

        public IEnumerable<User> GetAllUser(bool trackChanges)
        {
            return _manager.User.GetAllUser(trackChanges);
        }

        public User GetUserById(int id, bool trackChanges)
        {
            var user = _manager.User.GetUserByIdAsync(id, trackChanges);
            if(user is null)
            {
                throw new UserNotFoundException(id);
            }
            return _manager.User.GetUserByIdAsync(id, trackChanges);
        }

        public void UpdateUser(int id, UserDtoForUpdate userDto, bool trackChanges)
        {
            var entity = GetUserById(id, trackChanges);
            if(entity is not null)
            {
                entity = _mapper.Map(userDto, entity);
                _manager.User.Update(entity);
                _manager.Save();

            }
            else
            {
                throw new UserNotFoundException(id);
            }
        }
    }
}

