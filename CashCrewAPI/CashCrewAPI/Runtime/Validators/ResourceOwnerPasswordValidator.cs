using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Services.Contracts;

namespace CashCrewAPI.Runtime.Validators
{
    public class ValidationFilterAttribute : IResourceOwnerPasswordValidator
    {
        private readonly IServiceManager _manager;

        public ValidationFilterAttribute(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                 if (await _manager.UserService.ValidateCredentialsAsync(context.UserName, context.Password))
                 {
                    var user = await _manager.UserService.GetUserByUsernameAsync(context.UserName,false);
                    var claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Id, user.ID.ToString()),
                        new Claim(JwtClaimTypes.NickName, user.Username),
                        new Claim(JwtClaimTypes.Subject, user.IBAN),
                    };

                    context.Result = new GrantValidationResult(context.UserName, "Password", claims);
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "An error occurred while processing your request");
            }
        }
    }
}
