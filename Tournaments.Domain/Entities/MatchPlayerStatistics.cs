
namespace Tournaments.Domain.Entities
{
   public class MatchPlayerStatistics
   {
      public Player Player { get; set; }
      public int GoalsDone { get; set; }
      public int YellowCards { get; set; }
      public int RedCards { get; set; }
      public int GoalsRecieved { get; set; } // Only if goalkeeper
   }
}
