using Microsoft.AspNetCore.Identity;
using ProjectFit.Entities;

namespace ProjectFit.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUp(SignUp SignUpObj);
        Task<string> SignIn(SignIn SignInObj);
    }
}
