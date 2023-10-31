﻿using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IVacationRepository: IRepositoryBase<Vacation>
    {
        void CreateVacation(Vacation vacation);
        Task<Vacation> GetVacationByTitleAndDescAsync(string title, string description, bool trackChanges);
        Task<List<Vacation>> GetVacationsByUserIdAsync(int id);

    }
}
