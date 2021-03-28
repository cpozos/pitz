using System.Collections.Generic;

namespace Tournaments.Domain.Entities
{
   public class MatchTeam
   {
      public Team Team { get; set; }
      public IEnumerable<MatchPlayerStatistics> MatchResults { get; set; }
   }
}