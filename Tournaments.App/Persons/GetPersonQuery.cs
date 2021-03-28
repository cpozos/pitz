using System.Threading;
using System.Threading.Tasks;
using Tournaments.App.Repositories;

namespace Tournaments.App.Persons
{
   public record GetPersonQuery(int Id, string Name) : IRequestWrapper<PersonDTO>
   {
   }

   public class GetPersonQueryHandler : IHandlerWrapper<GetPersonQuery, PersonDTO>
   {
      private readonly IPersonRepository _repository;

      public GetPersonQueryHandler(IPersonRepository repository)
      {
         _repository = repository;
      }

      public Task<Response<PersonDTO>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
      {
         var person = _repository.GetPerson(request);

         if (person == null)
         {
            return Task.FromResult(Response.Fail<PersonDTO>("Error returning Person"));
         }

         if (string.IsNullOrWhiteSpace(person.FirstName))
         {
            return Task.FromResult(Response.Fail<PersonDTO>("Error returning Person"));
         }

         return Task.FromResult(Response.Ok(person));
      }
   }
}
