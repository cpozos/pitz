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

      public Task<DataResponse<User>> GetUserByIdAsync(int id)
      {
         throw new System.NotImplementedException();
      }

      public Task<DataResponse<User>> GetUserByEmailAsync(string email)
      {
         throw new System.NotImplementedException();
      }
   }
}
