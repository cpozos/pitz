
namespace Users.Domain
{
   public class Credentials
   {
      public PitzCredentials Pitz { get; set; }
      public FacebookCredentials Facebook { get; set; }
   }

   public class PitzCredentials
   {

      public string Email { get; set; }
      public string Password { get; set; }
   }

   public class FacebookCredentials
   {

      public string FacebookId { get; set; }
   }

}
