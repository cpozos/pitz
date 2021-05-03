using System.Threading.Tasks;
using Pitz.Identity.App.Contracts;
using Pitz.Identity.Domain;

namespace Pitz.Identity.App.Repositories
{
   public interface IUserRepository
   {
      Task<DataResponse<User>> GetUserByIdAsync(int id);
      Task<DataResponse<User>> GetUserByEmailAsync(string email);
      Task<DataResponse<User>> AddUserAsync(BasicRegisterRequest request);
   }
}