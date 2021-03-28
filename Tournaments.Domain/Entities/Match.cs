using System;

namespace Tournaments.Domain.Entities
{
   public class Match
   {
      public int Id { get; set; }
      public MatchTeam HomeTeam { get; set; }
      public MatchTeam VisitingTeam { get; set; }
      public DateTime DateToBePlayed { get; set; }
      public DateTime DatePlayed { get; set; }
   }
}