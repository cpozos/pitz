using System.Collections.Generic;
using Tournaments.Domain.Enums;

namespace Tournaments.Domain.Entities
{
   public class OrganizationIntegrant : Person
   {
      public List<OrganizerRol> Rols { get; set; }
   }
}