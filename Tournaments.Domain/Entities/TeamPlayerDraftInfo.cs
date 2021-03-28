using System;

namespace Tournaments.Domain.Entities
{
   public class TeamPlayerDraftInfo
   {
      public DateTime Start { get; set; }
      public DateTime Finish { get; set; }
      public decimal PaymentPerMatch { get; set; }
   
   }
}