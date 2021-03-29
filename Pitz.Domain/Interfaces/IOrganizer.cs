using System.Collections.Generic;
using Pitz.Domain.Entities;

namespace Pitz.Domain.Interfaces
{
   public interface IOrganizer
   {
      int Id { get; set; }
      string Name { get; set; }
      ICollection<Tournament> OrginizedPitz { get; set; }
   }
}