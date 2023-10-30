using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface ILoginService
    {
        Task<LoginResponseModel> LoginAsync(LoginDto user);
    }
}

