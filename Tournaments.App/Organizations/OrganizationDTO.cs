using System.Collections.Generic;
using Tournaments.Domain.Entities;

namespace Tournaments.App.Organizations
{
   public record OrganizationDTO (int Id, string Name, IEnumerable<OrganizationIntegrant> Integrants, IEnumerable<Tournament> OrganizedTournaments);
}