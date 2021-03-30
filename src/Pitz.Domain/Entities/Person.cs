namespace Pitz.Domain.Entities
{
   public class Person : User
   {
      public string FirstName { get; set; }
      public string MiddleName { get; set; }
      public string LastName { get; set; }
   }
}