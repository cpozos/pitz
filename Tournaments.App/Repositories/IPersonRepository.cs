using System.Collections.Generic;
using System.Threading.Tasks;
using Tournaments.App.Persons;
using Tournaments.Domain.Entities;

namespace Tournaments.App.Repositories
{
   public interface IPersonRepository
   {
      Task<Response<PersonDTO>> AddAsync(CreatePersonCommand query);

      PersonDTO GetPerson(GetPersonQuery query);

      Person GetPersonById(int id);

      IEnumerable<PersonDTO> GetUsers(GetPersonsQuery query);
   }
}