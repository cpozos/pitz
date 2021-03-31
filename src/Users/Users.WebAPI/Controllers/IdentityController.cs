using Microsoft.AspNetCore.Mvc;

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
      private readonly JwtService _jwtService;

      public IdentityController(IUserRepository userRepository, JwtService jwtService)
      {
         _userRepository = userRepository;
         _jwtService = jwtService;
      }

      [HttpPost]
      public async Task<IActionResult> Register([FromBody] BasicRegisterRequest request)
      {
         if (!ModelState.IsValid)
            return BadRequest();

         var result = await _userRepository.AddUserAsync(request);
         if (!result.Succeed)
            return Problem(result.Errors);

         var token = _jwtService.Generate(result.Data.Id, request.Name, request.Email);

         if (string.IsNullOrWhiteSpace(token))
         {
            return BadRequest();
         }

         Response.Cookies.Append("pritz_jwt", token, new Microsoft.AspNetCore.Http.CookieOptions
         {
         });

         return Ok(new { accessToken = token });
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
