using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Pitz.App;
using Pitz.App.Organizations;
using Microsoft.AspNetCore.Authorization;

namespace Pitz.WebAPI.Controllers
{
   [ApiController]
   [Route("[controller]")]
   [Authorize]
   public class OrganizationsController : ControllerBase
   {
      private readonly IMediator _mediator;

      public OrganizationsController(IMediator mediator)
      {
         _mediator = mediator;
      }

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
