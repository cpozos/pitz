using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.App;
using Tournaments.App.Persons;
using Tournaments.App.Repositories;
using Tournaments.Domain.Entities;

namespace Tournaments.Infraestructure
{
   public class PersonRepository : IPersonRepository
   {
      public Task<Response<PersonDTO>> AddAsync(CreatePersonCommand query)
      {
         // Creates entity
         var person = new Person
         {
            Id = PeopleDB.Items.Count + 1,
            FirstName = query.FirstName,
            MiddleName = query.MiddleName,
            LastName = query.LastName,
         };

         // Insert it
         PeopleDB.Items.Add(person);

         // Returns DTO
         return Task.FromResult(Response.Ok(new PersonDTO
         {
            Id = person.Id,
            FirstName = query.FirstName,
            MiddleName = query.MiddleName,
            LastName = query.LastName
         }));
      }

      public PersonDTO GetPerson(GetPersonQuery query)
      {
         var person = PeopleDB.Items.Find(p => p.Id == query.Id);

         if (person == null)
            return new PersonDTO();


         return new PersonDTO
         {
            Id = person.Id,
            FirstName = person.FirstName,
            MiddleName = person.MiddleName,
            LastName = person.LastName
         };
      }

      public Person GetPersonById(int id)
      {
         var person = PeopleDB.Items.Find(p => p.Id == id);
         return person;
      }

      public IEnumerable<PersonDTO> GetUsers(GetPersonsQuery query)
      {
         return PeopleDB.Items.Select(p => new PersonDTO
         {
            Id = p.Id,
            FirstName = p.FirstName,
            MiddleName = p.MiddleName,
            LastName = p.LastName
         }) ?? new List<PersonDTO>();
      }
   }
}
