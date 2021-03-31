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
      //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
      //[DataType(DataType.Password)]
      public string Password { get; set; }
   }
}