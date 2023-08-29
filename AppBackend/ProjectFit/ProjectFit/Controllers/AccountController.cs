using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectFit.Entities;
using ProjectFit.Interfaces;

namespace ProjectFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUp SignUpObj)
        {
            var result = await _accountRepository.SignUp(SignUpObj);
            if (result.Succeeded)
            {
                return Ok("User Account Created");
            }
            return BadRequest("Failed to create account");
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignIn SignInObj)
        {
            var token = await _accountRepository.SignIn(SignInObj);
            if(!string.IsNullOrEmpty(token) && token!= "Incorrect Email/Password")
            {
                return Ok(token);
            }
            return BadRequest("Failed to Login");

        }

        [HttpPost]
        [Route("SignUpForCoach")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> SignUpForCoach([FromBody] SignUp SignUpObj)
        {
            var result = await _accountRepository.SignUpForCoach(SignUpObj);
            if(result.Succeeded)
            {
                return Ok("Coach Account Created");
            }
            return BadRequest("Failed to create coach account");
        }

        [HttpPost]
        [Route("SignUpForAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SignUpForAdmin([FromBody] SignUp SignUpObj)
        {
            var result = await _accountRepository.SignUpForAdmin(SignUpObj);
            if(result.Succeeded)
            {
                return Ok("Admin Account created");
            }
            return BadRequest("Failed to create Admin Account");
        }
    }
}
