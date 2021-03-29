using System.Collections.Generic;

namespace Pitz.Domain.Entities
{
   public class MatchTeam
   {
      public Team Team { get; set; }
      public IEnumerable<MatchPlayerStatistics> MatchResults { get; set; }
   }
}