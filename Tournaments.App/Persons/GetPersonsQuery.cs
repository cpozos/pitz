using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tournaments.App.Repositories;

namespace Tournaments.App.Persons
{
   public class GetPersonsQuery : IRequestWrapper<IEnumerable<PersonDTO>>
   {
   }

   public class GetPersonsQueryHandler : IHandlerWrapper<GetPersonsQuery, IEnumerable<PersonDTO>>
   {
      private readonly IPersonRepository _repository;

      public GetPersonsQueryHandler(IPersonRepository repository) => _repository = repository;

      public Task<Response<IEnumerable<PersonDTO>>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
      {
         var people = _repository.GetUsers(request);
         return Task.FromResult(Response.Ok(people));
      }
   }
}
