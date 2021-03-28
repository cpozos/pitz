using System.Collections.Generic;
using Tournaments.Domain.Interfaces;

namespace Tournaments.Domain.Entities
{
   public class Organization : IOrganizer
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public ICollection<Tournament> OrginizedTournaments { get; set; } = new List<Tournament>();
      public ICollection<OrganizationIntegrant> Integrants { get; set; } = new List<OrganizationIntegrant>();
   }
}