namespace Pitz.App.Persons
{
   public class PersonDTO
   {
      public int? Id { get; set; }
      public string FirstName { get; set; } = string.Empty;
      public string MiddleName { get; set; } = string.Empty;
      public string LastName { get; set; } = string.Empty;
      public string FullName => $"{FirstName}{(string.IsNullOrWhiteSpace(MiddleName) ? " " : $" {MiddleName} ")}{LastName}";
   }
}
