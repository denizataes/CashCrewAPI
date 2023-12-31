﻿using System;
namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        ILoginRepository Login { get; }
        IVacationRepository Vacation { get; }
        IVacationUserAssociationRepository VacationUserAssociation { get; }
        IPaymentRepository Payment { get; }
        IDebtRepository Debt { get; }
        IPaymentParticipantRepository PaymentParticipant { get; }
        Task SaveAsync();
    }
}

