using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Users.App;
using Users.App.Services;

namespace Users.WebAPI.Controllers
{
   [ApiController]
   [Route("[controller]/[action]")]
   public class IdentityController : ControllerBase
   {
      private readonly IUserRepository _userRepository;

      public IdentityController(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }

      [HttpPost]
      public async Task<IActionResult> Register([FromBody] BasicRegisterRequest request)
      {
         if (!ModelState.IsValid)
            return BadRequest();

         var result = await _userRepository.AddUserAsync(request);
         if (!result.Succeed)
            return Problem(result.Errors);

         return BuildToken(request.Email);
      }

      private IActionResult BuildToken(string email)
      {
         try
         {
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Email, email),
               new Claim(JwtRegisteredClaimNames.UniqueName, "id")
            };
            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;
            var signCred = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
               Constants.Issuer,
               Constants.Audiance,
               claims,
               notBefore: DateTime.Now,
               expires: DateTime.Now.AddHours(2),
               signCred);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { accessToken = tokenJson }); ;
         }
         catch(Exception e)
         {
            var a = e.Message;
            return BadRequest();
         }
      }

      //private readonly UserManager<IdentityUser> _userManager;
      //private readonly SignInManager<IdentityUser> _signInManager;
      //private readonly IEmailService _emailService;

      //public IdentityController(
      //   UserManager<IdentityUser> userManager,
      //   SignInManager<IdentityUser> signInManager,
      //   IEmailService emailservice)
      //{
      //   _userManager = userManager;
      //   _signInManager = signInManager;
      //   _emailService = emailservice;
      //}

      //[HttpPost]
      //public async Task<IActionResult> LogIn(string username, string password)
      //{
      //   var user = await _userManager.FindByNameAsync(username);

      //   if (user != null)
      //   {
      //      // Sign in
      //      var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

      //      if (signInResult.Succeeded)
      //      {
      //         return Ok();
      //      }
      //   }

      //   return NotFound();
      //}

      //[HttpPost]
      //public async Task<IActionResult> Register(string username, string password)
      //{
      //   var user = new IdentityUser
      //   {
      //      UserName = username,
      //      Email = ""
      //      //,PasswordHash = "custom hash"
      //   };

      //   var result = await _userManager.CreateAsync(user, password);
      //   if (!result.Succeeded)
      //      return NotFound();

      //   var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
      //   var link = Url.Action(nameof(VerifyEmail), "Home", new { userId = user.Id, code = token }, Request.Scheme, Request.Host.ToString());
      //   await _emailService.SendAsync("test@test.com", "Email verify", $"<a href=\"{link}\">Click to verify</a>", true);
      //   return Ok();
      //}

      //public async Task<IActionResult> VerifyEmail(string userId, string code)
      //{
      //   var user = await _userManager.FindByIdAsync(userId);

      //   // Do not return information about the error (hackers)
      //   if (user is null) return BadRequest();

      //   var result = await _userManager.ConfirmEmailAsync(user, code);

      //   if (result.Succeeded)
      //   {
      //      return Ok();
      //   }

      //   return BadRequest();
      //}

      //public IActionResult EmailVerification()
      //{
      //   return Ok();
      //}
   }
}
