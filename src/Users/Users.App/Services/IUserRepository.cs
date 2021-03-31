using System.Threading.Tasks;
using Users.Domain;

namespace Users.App.Services
{
   public interface IUserRepository
   {
      Task<DataResponse<User>> GetUserAsync(int id);

      Task<Response> AddUserAsync(BasicRegisterRequest request);
   }
}