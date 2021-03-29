using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pitz.App.Organizations;
using Pitz.Domain.Entities;

namespace Pitz.App.Repositories
{
   public interface IOrganizationRepository
   {
      Task<Organization> AddOrganizationAsync(CreateOrganizationCommand command);

      Task<IEnumerable<Organization>> GetOrganizations(Func<Organization,bool> filter);

      Task<Organization> GetOrganization(int id);
   }
}