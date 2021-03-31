using System;

namespace Pitz.Domain.Entities
{
   public class User 
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public DateTime Created { get; set; }
   }
}