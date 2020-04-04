using Identity.Api.Application.ViewModels;
using System.Threading.Tasks;

namespace Identity.Api.Services
{
    public interface IAccountService<TUser>
    {
        Task<bool> ValidateCredentials(TUser user, string password);

        Task<TUser> FindByUsername(string user);

        Task<LoginViewModel> Login(string email, string password);

        Task Register(TUser user, string password);
    }
}
