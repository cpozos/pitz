using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pitz.App.Repositories;

namespace Pitz.App.Persons
{
   public record GetPersonQuery(int Id, string Name) : IRequestWrapper<PersonDTO> { }
   public record GetPersonsQuery : IRequestWrapper<IEnumerable<PersonDTO>> { }

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
            return Task.FromResult(Response.Fail<PersonDTO>($"Person with id {request.Id} not found"));
         }

         return Task.FromResult(Response.Ok(person));
      }
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