using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Pitz.App;
using Pitz.App.Organizations;

namespace Pitz.WebAPI.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class OrganizationsController : ControllerBase
   {
      private readonly ILogger<OrganizationsController> _logger;
      private readonly IMediator _mediator;

      public OrganizationsController(ILogger<OrganizationsController> logger, IMediator mediator)
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
      [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<OrganizationDTO>))]
      public async Task<IActionResult> Create([FromBody] CreateOrganizationCommand request)
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
         var org = await _mediator.Send(new GetOrganizationQuery(id));
         if (org.WithError)
         {
            return NotFound(org);
         }

         return Ok(org);
      }

      [HttpGet]
      public async Task<IActionResult> Get()
      {
         var response = await _mediator.Send(new GetOrganizationsQuery());
         return Ok(response);
      }
   }
}
