using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tournaments.App;
using Tournaments.App.Persons;

namespace Tournaments.WebAPI.Controllers
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
      public Task<Response<PersonDTO>> Create([FromBody] CreatePersonCommand request)
      {
         return _mediator.Send(request);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet("{id}")]
      public Task<Response<PersonDTO>> Get(int id)
      {
         return _mediator.Send(new GetPersonQuery(id, ""));
      }

      [HttpGet]
      public Task<Response<IEnumerable<PersonDTO>>> Get()
      {
         return _mediator.Send(new GetPersonsQuery());
      }
   }
}
