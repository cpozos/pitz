namespace Pitz.Identity.Domain
{
   public class User
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public Credentials Credentials { get; set; }
   }
}