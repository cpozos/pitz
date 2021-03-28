using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Tournaments.WebAPI.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class PersonController : ControllerBase
   {
      private readonly ILogger<PersonController> _logger;

      public PersonController(ILogger<PersonController> logger)
      {
         _logger = logger;
      }

      [HttpGet]
      public IEnumerable<WeatherForecast> Get()
      {
         return null;
      }
   }
}
