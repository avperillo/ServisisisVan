using Identity.Api.Application.Commands;
using Identity.Api.Models;
using Identity.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService<ApplicationUser> loginService;

        public AccountController(IAccountService<ApplicationUser> loginService)
        {
            this.loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var user = await loginService.Login(command.Email, command.Password);

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = command.Email,
                UserName = command.Email
            };

            await loginService.Register(user, command.Password);

            return Ok();
        }
    }
}