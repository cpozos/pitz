using System.Collections.Generic;
using System.Threading.Tasks;
using Tournaments.App.Persons;

namespace Tournaments.App.Repositories
{
   public interface IPersonRepository
   {
      Task<Response<PersonDTO>> AddAsync(CreatePersonCommand query);

      PersonDTO GetPerson(GetPersonQuery query);

      IEnumerable<PersonDTO> GetUsers(GetPersonsQuery query);
   }
}
