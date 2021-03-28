using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.App.Organizations;
using Tournaments.App.Repositories;
using Tournaments.Domain.Entities;

namespace Tournaments.Infraestructure
{
   public class OrganizationRepository : IOrganizationRepository
   {
      public Task<Organization> AddOrganizationAsync(CreateOrganizationCommand command)
      {
         // Creates entity
         var person = new Organization
         {
            Id = OrganizationsDB.Items.Count + 1,
            Name = command.Name,
            Integrants = command.Integrants
         };

         // Insert it
         OrganizationsDB.Items.Add(person);

         return Task.FromResult(person);
      }

      public Task<Organization> GetOrganization(int id)
      {
         var org = OrganizationsDB.Items.Find(p => p.Id == id);
         return Task.FromResult(org);
      }

      public Task<IEnumerable<Organization>> GetOrganizations(Func<Organization, bool> filter)
      {
         return Task.FromResult(OrganizationsDB.Items.Where(item => filter(item)));
      }
   }
}
