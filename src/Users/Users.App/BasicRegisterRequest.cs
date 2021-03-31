using System.ComponentModel.DataAnnotations;

namespace Users.App
{
   public class BasicRegisterRequest
   {
      //[Required]
      //[MinLength(10)]
      public string Name { get; set; }

      //[Required]
      //[EmailAddress]
      public string Email { get; set; }

      //[Required]
      //[MinLength(8)]
      public string Password { get; set; }
   }
}