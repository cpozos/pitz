using System;
using System.Collections.Generic;
using Tournaments.Domain.Interfaces;

namespace Tournaments.Domain.Entities
{
   public class Tournament
   {
      public int Id { get; set; }
      public string Title { get; set; }
      public IOrganizer Organizer { get; set; }

      public DateTime TimeCreated { get; set; }
      public DateTime TimeStarted { get; set; }
      public DateTime TimeFinished { get; set; }

      public Calendar Calendar { get; set; }
      public List<Team> Teams { get; set; }
   }
}