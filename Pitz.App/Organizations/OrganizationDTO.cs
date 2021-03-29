using System.Collections.Generic;
using Pitz.Domain.Entities;

namespace Pitz.App.Organizations
{
   public record OrganizationDTO (int Id, string Name, IEnumerable<OrganizationIntegrant> Integrants, IEnumerable<Tournament> OrganizedPitz);
}