using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tournaments.App.Organizations;
using Tournaments.Domain.Entities;

namespace Tournaments.App.Repositories
{
   public interface IOrganizationRepository
   {
      Task<Organization> AddOrganizationAsync(CreateOrganizationCommand command);

      Task<IEnumerable<Organization>> GetOrganizations(Func<Organization,bool> filter);

      Task<Organization> GetOrganization(int id);
   }
}