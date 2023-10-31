using System;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IVacationRepository _vacationRepository;

        public RepositoryManager(RepositoryContext context,
            IUserRepository userRepository,
            IVacationRepository vacationRepository
            )
        {
            _context = context;
            _userRepository = userRepository;
            _vacationRepository = vacationRepository;
        }

        public IUserRepository User => _userRepository;

        public ILoginRepository Login => throw new NotImplementedException();

        public IVacationRepository Vacation => _vacationRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

