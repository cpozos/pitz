using System.Threading.Tasks;
using Users.Domain;

namespace Users.App.Services
{
   public interface IUserRepository
   {
      Task<DataResponse<User>> GetUserByIdAsync(int id);
      Task<DataResponse<User>> GetUserByEmailAsync(string email);
      Task<DataResponse<User>> AddUserAsync(BasicRegisterRequest request);
   }
}