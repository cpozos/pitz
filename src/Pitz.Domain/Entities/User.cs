using System;

namespace Pitz.Domain.Entities
{
   public class User 
   {
      public string Id { get; set; }
      public string Name { get; set; }
      public DateTime Created { get; set; }
   }
}