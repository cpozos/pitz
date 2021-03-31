using Pitz.Domain.Entities;

namespace Pitz.App.Services
{
   public interface IUsersService
   {
      Person GetPersonById(int id);
   }
}
