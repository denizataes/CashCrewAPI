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

        public async Task<UserDto> CreateUserAsync(UserDtoForInsertion userDto)
        {
            var entity = _mapper.Map<User>(userDto);
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _manager.User.CreateUser(entity);
            await _manager.SaveAsync();
            return _mapper.Map<UserDto>(entity);
        }

        public async Task DeleteUserAsync(int id)
        {
            var entity = await _manager.User.GetUserByIdAsync(id, false);
            if (entity is null)
            {
                _logger.LogInfo("User does not exists.");
                throw new UserNotFoundException(id);
            }

            _manager.User.DeleteUser(entity);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync(bool trackChanges)
        {
            var user = await _manager.User.GetAllUserAsync(trackChanges);
            return _mapper.Map<IEnumerable<UserDto>>(user);
        }

        public async Task<UserDto> GetUserByIdAsync(int id, bool trackChanges)
        {
            var user = await _manager.User.GetUserByIdAsync(id, trackChanges);
            if(user is null)
            {
                throw new UserNotFoundException(id);
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(int id, UserDtoForUpdate userDto, bool trackChanges)
        {
            var entity = await _manager.User.GetUserByIdAsync(id, false);
            if(entity is not null)
            {
                entity = _mapper.Map<User>(userDto);
                _manager.User.Update(entity);
                await _manager.SaveAsync();
            }
            else
            {
                throw new UserNotFoundException(id);
            }
        }
    }
}

