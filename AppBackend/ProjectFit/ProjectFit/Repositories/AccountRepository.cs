using Microsoft.AspNetCore.Identity;
using ProjectFit.Entities;
using ProjectFit.Interfaces;

namespace ProjectFit.Repositories
{
    public static class UserRole
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Coach = "Coach";
    }
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AccountRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ILogger logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        public Task<string> SignIn(SignIn SignInObj)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> SignUp(SignUp SignUpObj)
        {
            var UserExists = await _userManager.FindByEmailAsync(SignUpObj.Email);
            if(UserExists != null)
            {
                return IdentityResult.Failed();
            }
            User user = new()
            {
                Email = SignUpObj.Email,
                UserName = SignUpObj.Email,
                FirstName = SignUpObj.Firstname,
                LastName = SignUpObj.Lastname,
                CountryCode = SignUpObj.CountryCode,
                PhoneNumber = SignUpObj.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var CreateUser = await _userManager.CreateAsync(user,SignUpObj.Password);
            if (!CreateUser.Succeeded)
            {
                return IdentityResult.Failed();
            }
            if(! await _roleManager.RoleExistsAsync(UserRole.Customer))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Customer));
            }
            if(await _roleManager.RoleExistsAsync(UserRole.Customer))
            {
                await _userManager.AddToRoleAsync(user,UserRole.Customer);
            }
            _logger.LogInformation($"{DateTimeOffset.UtcNow} INFO: Registration completed for {SignUpObj.Email}");
            return IdentityResult.Success;

        }
    }
}
