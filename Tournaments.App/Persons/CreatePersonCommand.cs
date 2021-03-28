using System.Threading;
using System.Threading.Tasks;
using Tournaments.App.Repositories;

namespace Tournaments.App.Persons
{
   public record CreatePersonCommand(string FirstName, string MiddleName, string LastName, string Email) 
      : IRequestWrapper<PersonDTO>
   {
   }

   public class CreatePersonCommandHandler : IHandlerWrapper<CreatePersonCommand, PersonDTO>
   {
      private readonly IPersonRepository _repository;

      public CreatePersonCommandHandler(IPersonRepository repository)
      {
         _repository = repository;
      }
      public Task<Response<PersonDTO>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
      {
         return _repository.AddAsync(request);
      }
   }
}
