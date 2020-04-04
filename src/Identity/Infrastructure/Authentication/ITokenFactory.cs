using Identity.Api.Models;
using System;

namespace Identity.Api.Infrastructure.Authentication
{
    public interface ITokenFactory
    {
        string Create(ApplicationUser user, DateTime expires);
    }
}