using System.Collections.Generic;
using Tournaments.Domain.Interfaces;

namespace Tournaments.Domain.Entities
{
   public class Organization : IOrganizer
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public List<OrganizationIntegrant> Integrants { get; set; } = new List<OrganizationIntegrant>();
      public List<Tournament> OrginizedTournaments { get; set; } = new List<Tournament>();
   }

   public class PersonOrganizer : Person, IOrganizer
   {
      public string Name { get; set; }
      public List<Tournament> OrginizedTournaments { get; set; } = new List<Tournament>();
   }
}
