using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Users.App;
using Users.App.Services;

namespace Users.WebAPI.Controllers
{
   [Route("[controller]")]
   [ApiController]
   public class UsersController : ControllerBase
   {
      private readonly IUserRepository _userRepository;
      private readonly JwtService _jwtService;

      public UsersController(IUserRepository userRepository,  JwtService jwtService)
      {
         _userRepository = userRepository;
         _jwtService = jwtService;
      }

      [HttpGet]
      public async Task<IActionResult> GetUser(int id)
      {
         var jwt = Request.Cookies["pitz_jwt"];
         var token = _jwtService.Verify(jwt);
         var claim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

         if (claim is null)
            return BadRequest();

         var email = claim.Value;
         if (string.IsNullOrWhiteSpace(email))
            return Problem();

         var user = await _userRepository.GetUserByEmailAsync(email);
         if (user is null)
            return NotFound();

         return Ok(user);
      }
   }
}
