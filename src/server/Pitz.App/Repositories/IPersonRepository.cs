using System.Collections.Generic;
using System.Threading.Tasks;
using Pitz.App.Persons;
using Pitz.Domain.Entities;

namespace Pitz.App.Repositories
{
   public interface IPersonRepository
   {
      Task<Response<PersonDTO>> AddAsync(CreatePersonCommand query);

      PersonDTO GetPerson(GetPersonQuery query);

      Person GetPersonById(int id);

      IEnumerable<PersonDTO> GetUsers(GetPersonsQuery query);
   }
}