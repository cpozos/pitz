using System.Collections.Generic;
using Pitz.Domain.Interfaces;

namespace Pitz.Domain.Entities
{
   public class Organization : IOrganizer
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public ICollection<Tournament> OrginizedPitz { get; set; } = new List<Tournament>();
      public ICollection<OrganizationIntegrant> Integrants { get; set; } = new List<OrganizationIntegrant>();
   }
}