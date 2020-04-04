using System;

namespace Identity.Api.Application.ViewModels
{
    public class LoginViewModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public DateTime Expires { get; set; }
        public string Token { get; set; }
    }
}
