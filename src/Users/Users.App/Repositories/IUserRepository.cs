using System.Threading.Tasks;
using Users.App.Contracts;
using Users.Domain;

namespace Users.App.Repositories
{
   public interface IUserRepository
   {
      Task<DataResponse<User>> GetUserByIdAsync(int id);
      Task<DataResponse<User>> GetUserByEmailAsync(string email);
      Task<DataResponse<User>> AddUserAsync(BasicRegisterRequest request);
   }
}