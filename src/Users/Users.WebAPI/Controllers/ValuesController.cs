using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.App.Services;

namespace Users.WebAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ValuesController : ControllerBase
   {
      private readonly IUserRepository _userRepository;

      public ValuesController(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }

      [HttpGet]
      public IActionResult Reg()
      {
         return Ok();
      }
   }
}
