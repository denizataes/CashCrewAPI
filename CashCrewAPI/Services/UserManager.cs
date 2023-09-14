using System;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
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
            var entity = await GetUserByIdAndCheckExists(id, false);
            _manager.User.DeleteUser(entity);
            await _manager.SaveAsync();
        }

        public async Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUserAsync(UserParameters userParameters ,bool trackChanges)
        {
            var usersWithMetaData = await _manager.User.GetAllUserAsync(userParameters, trackChanges);
            var usersDto =  _mapper.Map<IEnumerable<UserDto>>(usersWithMetaData);
            return (usersDto, usersWithMetaData.MetaData);
        }

        public async Task<UserDto> GetUserByIdAsync(int id, bool trackChanges)
        {
            var entity = await GetUserByIdAndCheckExists(id, false);
            return _mapper.Map<UserDto>(entity);
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

        private async Task<User> GetUserByIdAndCheckExists(int id, bool trackChanges)
        {
            // check entity 
            var entity = await _manager.User.GetUserByIdAsync(id, trackChanges);

            if (entity is null)
                throw new UserNotFoundException(id);

            return entity;
        }
    }
}

