
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pitz.App.Repositories;

namespace Pitz.App.Organizations
{
   public record GetOrganizationQuery(int Id) : IRequestWrapper<OrganizationDTO> { }

   public record GetOrganizationsQuery : IRequestWrapper<IEnumerable<OrganizationDTO>> { }

   public class GetOrganizationQueryHandler : IHandlerWrapper<GetOrganizationQuery, OrganizationDTO>
   {
      private readonly IOrganizationRepository _repository;

      public GetOrganizationQueryHandler(IOrganizationRepository repository) => _repository = repository;

      public async Task<Response<OrganizationDTO>> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
      {
         var res = await _repository.GetOrganization(request.Id);
         if (res is null)
         {
            return Response.Fail<OrganizationDTO>($"Organization with id {request.Id} not found");
         }

         return Response.Ok(new OrganizationDTO(res.Id, res.Name, res.Integrants, res.OrginizedPitz));
      }
   }

   public class GetOrganizationsQueryHandler : IHandlerWrapper<GetOrganizationsQuery, IEnumerable<OrganizationDTO>>
   {
      private readonly IOrganizationRepository _repository;

      public GetOrganizationsQueryHandler(IOrganizationRepository repository) => _repository = repository;

      public async Task<Response<IEnumerable<OrganizationDTO>>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
      {
         var res = await _repository.GetOrganizations(_ =>
         {
            return true;
         });

         var orgs = res.Select(org => new OrganizationDTO(org.Id, org.Name, org.Integrants, org.OrginizedPitz));

         return Response.Ok(orgs);
      }
   }
}