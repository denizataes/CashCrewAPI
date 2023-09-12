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

        public UserDto CreateUser(UserDtoForInsertion userDto)
        {
            var entity = _mapper.Map<User>(userDto);
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _manager.User.CreateUser(entity);
            _manager.Save();
            return _mapper.Map<UserDto>(entity);
        }

        public void DeleteUser(int id)
        {
            var entity = _manager.User.GetUserByIdAsync(id, false);
            if (entity is null)
            {
                _logger.LogInfo("User does not exists.");
                throw new UserNotFoundException(id);
            }

            _manager.User.DeleteUser(entity);
            _manager.Save();
        }

        public IEnumerable<UserDto> GetAllUser(bool trackChanges)
        {
            var user = _manager.User.GetAllUser(trackChanges);
            return _mapper.Map<IEnumerable<UserDto>>(user);
        }

        public UserDto GetUserById(int id, bool trackChanges)
        {
            var user = _manager.User.GetUserByIdAsync(id, trackChanges);
            if(user is null)
            {
                throw new UserNotFoundException(id);
            }
            return _mapper.Map<UserDto>(user);
        }

        public void UpdateUser(int id, UserDtoForUpdate userDto, bool trackChanges)
        {
            var entity = _manager.User.GetUserByIdAsync(id, false);
            if(entity is not null)
            {
                entity = _mapper.Map<User>(userDto);
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

