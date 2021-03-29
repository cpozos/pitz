using System.Collections.Generic;
using Pitz.Domain.Enums;

namespace Pitz.Domain.Entities
{
   public class OrganizationIntegrant : Person
   {
      public List<OrganizerRol> Rols { get; set; }
   }
}