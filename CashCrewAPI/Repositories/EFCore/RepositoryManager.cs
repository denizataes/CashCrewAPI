using System;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _context;
        private readonly IUserRepository _userRepository;

        public RepositoryManager(RepositoryContext context,
            IUserRepository userRepository
            )
        {
            _context = context;
            _userRepository = userRepository;
        }

        public IUserRepository User => _userRepository;

        public ILoginRepository Login => throw new NotImplementedException();

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

