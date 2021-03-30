using System.Collections.Generic;

namespace Pitz.Domain.Entities
{
   public class Calendar
   {
      public int Id { get; set; }
      public ICollection<Match> Matches { get; set; }
   }
}
