using Identity.Api.Application.ViewModels;
using Identity.Api.Infrastructure.Authentication;
using Identity.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{
    public class AccountService : IAccountService<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenFactory tokenFactory;
        private readonly IConfiguration configuration;

        public AccountService(UserManager<ApplicationUser> userManager, ITokenFactory tokenFactory, IConfiguration configuration)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.tokenFactory = tokenFactory ?? throw new ArgumentNullException(nameof(tokenFactory));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<ApplicationUser> FindByUsername(string user)
        {
            return await userManager.FindByEmailAsync(user);
        }

        public async Task<LoginViewModel> Login(string email, string password)
        {
            var user = await FindByUsername(email) ?? throw new Exception("Invalid user or password"); ;

            if (!await ValidateCredentials(user, password))
                throw new Exception("Invalid user or password");

            DateTime tokenExpiration = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("TokenExpireMinutes", 120));

            var loginViewModel = new LoginViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Expires = tokenExpiration,
                Token = tokenFactory.Create(user, tokenExpiration)
            };

            return loginViewModel;
        }

        public async Task Register(ApplicationUser user, string password)
        {
            var result = await userManager.CreateAsync(user, password);

            if(!result.Succeeded)
            {
                string message = string.Join(", ", result.Errors.Select(e => $"[{e.Code}] - {e.Description}"));
                throw new Exception(message);
            }
        }

        public async Task<bool> ValidateCredentials(ApplicationUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

    }
}
