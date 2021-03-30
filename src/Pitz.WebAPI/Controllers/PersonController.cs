using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Pitz.App;
using Pitz.App.Persons;
using Microsoft.AspNetCore.Authorization;

namespace Pitz.WebAPI.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class PersonsController : ControllerBase
   {
      private readonly ILogger<PersonsController> _logger;
      private readonly IMediator _mediator;

      public PersonsController(ILogger<PersonsController> logger, IMediator mediator)
      {
         _logger = logger;
         _mediator = mediator;
      }

      /// <summary>
      /// It is used to add a new User
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpPost]
      [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<PersonDTO>))]
      public async Task<IActionResult> Create([FromBody] CreatePersonCommand request)
      {
         var response = await _mediator.Send(request);
         if (response.WithError)
         {
            return BadRequest(response);
         }

         return Ok(response);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet("{id}")]
      public async Task<IActionResult> Get(int id)
      {
         var response = await _mediator.Send(new GetPersonQuery(id, ""));
         if (response.Data is null)
         {
            return NotFound(response);
         }

         return Ok(response);
      }

      [HttpGet]
      public async Task<IActionResult> Get()
      {
         var response = await _mediator.Send(new GetPersonsQuery());
         return Ok(response);
      }

      [HttpPost]
      [AllowAnonymous]
      public async Task<IActionResult> LogIn([FromBody] UserAuth request)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         return Ok();
      }
   }
}
