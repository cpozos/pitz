using System.Threading.Tasks;
using Users.App;
using Users.App.Services;
using Users.Domain;

namespace Users.Infraestructure
{
   public class UserRepository : IUserRepository
   {
      public Task<Response> AddUserAsync(BasicRegisterRequest request)
      {
         UsersDB.Add(new User
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
         });

         return Task.FromResult( new Response(true));
      }

      public Task<DataResponse<User>> GetUserAsync(int id)
      {
         throw new System.NotImplementedException();
      }
   }
}
