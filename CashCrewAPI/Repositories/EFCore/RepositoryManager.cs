using System;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IVacationRepository _vacationRepository;
        private readonly IVacationUserAssociationRepository _vacationUserRepository;
        private readonly IPaymentParticipantRepository _paymentParticipantRepository;

        public RepositoryManager(RepositoryContext context,
            IUserRepository userRepository,
            IVacationRepository vacationRepository,
            IVacationUserAssociationRepository vacationUserRepository,
            IPaymentRepository paymentRepository,
            IPaymentParticipantRepository paymentParticipantRepository
            )
        {
            _context = context;
            _userRepository = userRepository;
            _vacationRepository = vacationRepository;
            _vacationUserRepository = vacationUserRepository;
            _paymentRepository = paymentRepository;
            _paymentParticipantRepository = paymentParticipantRepository;
        }

        public IUserRepository User => _userRepository;

        public ILoginRepository Login => throw new NotImplementedException();

        public IVacationRepository Vacation => _vacationRepository;

        public IPaymentRepository Payment => _paymentRepository;

        public IPaymentParticipantRepository PaymentParticipant => _paymentParticipantRepository;

        public IVacationUserAssociationRepository VacationUserAssociation => _vacationUserRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

