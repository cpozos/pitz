using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pitz.App.Repositories;
using Pitz.Domain.Entities;

namespace Pitz.App.Organizations
{
   public record CreateOrganizationCommand(string Name, ICollection<OrganizationIntegrant> Integrants)
     : IRequestWrapper<OrganizationDTO>
   {
   }

   public class CreatePersonCommandHandler : IHandlerWrapper<CreateOrganizationCommand, OrganizationDTO>
   {
      private readonly IOrganizationRepository _repository;
      private readonly IPersonRepository _personRepository;

      public CreatePersonCommandHandler(IOrganizationRepository repository, IPersonRepository personRepository)
      {
         _repository = repository;
         _personRepository = personRepository;
      }
      public async Task<Response<OrganizationDTO>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
      {
         // Validations
         if (request.Integrants is null || request.Integrants.Count < 1)
         {

         }

         if (await ValidateIntegrantsAsync(request.Integrants) == false)
         {
            //TODO: Throw error person not registered
            return Response.Fail<OrganizationDTO>("One or more integrants are not registered yet, please check the status of all of them.");
         }

         var result = await _repository.AddOrganizationAsync(request);
         var response = Response.Ok(new OrganizationDTO(result.Id, result.Name, result.Integrants, result.OrginizedPitz));

         return response;
      }

      public Task<bool> ValidateIntegrantsAsync(ICollection<OrganizationIntegrant> integrants)
      {
         return Task.Run(() =>
         {
            var res = Parallel.ForEach(integrants, (integrant, status) =>
            {
               var person = _personRepository.GetPersonById(integrant.Id);
               if (person is null)
                  status.Stop();
            });

            return res.IsCompleted;
         });
      }
   }
}