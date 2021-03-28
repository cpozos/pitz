using System.Collections.Generic;
using Tournaments.Domain.Entities;

namespace Tournaments.Domain.Interfaces
{
   public interface IOrganizer
   {
      int Id { get; set; }
      string Name { get; set; }
      ICollection<Tournament> OrginizedTournaments { get; set; }
   }
}