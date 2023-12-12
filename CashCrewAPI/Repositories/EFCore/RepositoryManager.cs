using System;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IVacationRepository _vacationRepository;
        private readonly IVacationUserAssociationRepository _vacationUserRepository;

        public RepositoryManager(RepositoryContext context,
            IUserRepository userRepository,
            IVacationRepository vacationRepository,
            IVacationUserAssociationRepository vacationUserRepository
            )
        {
            _context = context;
            _userRepository = userRepository;
            _vacationRepository = vacationRepository;
            _vacationUserRepository = vacationUserRepository;
        }

        public IUserRepository User => _userRepository;

        public ILoginRepository Login => throw new NotImplementedException();

        public IVacationRepository Vacation => _vacationRepository;

        public IVacationUserAssociationRepository VacationUserAssociation => _vacationUserRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

