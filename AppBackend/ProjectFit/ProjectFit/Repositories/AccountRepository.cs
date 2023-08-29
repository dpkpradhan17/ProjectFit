using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProjectFit.Entities;
using ProjectFit.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public async Task<string> SignIn(SignIn SignInObj)
        {
            var user = await _userManager.FindByEmailAsync(SignInObj.Email);
            if (user == null)
            {
                _logger.LogWarning($"{DateTimeOffset.UtcNow} WAR : SignIn failed : {SignInObj.Email}");
                return "Incorrect Email/Password";
            }
            if(!await _userManager.CheckPasswordAsync(user, SignInObj.Password))
            {
                _logger.LogWarning($"{DateTimeOffset.UtcNow} WAR : SignIn failed : {SignInObj.Email}");
                return "Incorrect Email/Password";
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            _logger.LogInformation($"{DateTimeOffset.UtcNow} INFO: SignIn success {SignInObj.Email}");
            return token;

        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
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
            _logger.LogInformation($"{DateTimeOffset.UtcNow} INFO: Registration completed for User {SignUpObj.Email}");
            return IdentityResult.Success;

        }
        public async Task<IdentityResult> SignUpForCoach(SignUp SignUpObj)
        {
            var UserExists = await _userManager.FindByEmailAsync(SignUpObj.Email);
            if (UserExists != null)
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

            var CreateUser = await _userManager.CreateAsync(user, SignUpObj.Password);
            if (!CreateUser.Succeeded)
            {
                return IdentityResult.Failed();
            }
            if (!await _roleManager.RoleExistsAsync(UserRole.Coach))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Coach));
            }
            if (await _roleManager.RoleExistsAsync(UserRole.Coach))
            {
                await _userManager.AddToRoleAsync(user, UserRole.Coach);
            }
            _logger.LogInformation($"{DateTimeOffset.UtcNow} INFO: Registration completed for Coach {SignUpObj.Email}");
            return IdentityResult.Success;

        }
        public async Task<IdentityResult> SignUpForAdmin(SignUp SignUpObj)
        {
            var UserExists = await _userManager.FindByEmailAsync(SignUpObj.Email);
            if (UserExists != null)
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

            var CreateUser = await _userManager.CreateAsync(user, SignUpObj.Password);
            if (!CreateUser.Succeeded)
            {
                return IdentityResult.Failed();
            }
            if (!await _roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
            }
            if (await _roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRole.Admin);
            }
            _logger.LogInformation($"{DateTimeOffset.UtcNow} INFO: Registration completed for Admin {SignUpObj.Email}");
            return IdentityResult.Success;

        }
    }
}
