using System.Linq;
using System.Threading.Tasks;
using Users.App;
using Users.App.Services;
using Users.Domain;

namespace Users.Infraestructure
{
   public class UserRepository : IUserRepository
   {
      public Task<DataResponse<User>> AddUserAsync(BasicRegisterRequest request)
      {
         var user = new User
         {
            Name = request.Name,
            Credentials = new Credentials
            {
               Pitz = new PitzCredentials
               {
                  Email = request.Email,
                  Password = request.Password
               }
            }
         };

         UsersDB.Add(user);

         return Task.FromResult( new DataResponse<User>(true, user));
      }

      public Task<DataResponse<User>> GetUserByEmailAsync(string email)
      {
         var item = UsersDB.Items.FirstOrDefault(i => string.Equals(i.Credentials?.Pitz?.Email, email));
         return Task.FromResult(new DataResponse<User>(true, item));
      }

      public Task<DataResponse<User>> GetUserByIdAsync(int id)
      {
         var item = UsersDB.Items.FirstOrDefault(i => i.Id == id);
         return Task.FromResult(new DataResponse<User>(true, item));
      }
   }
}
