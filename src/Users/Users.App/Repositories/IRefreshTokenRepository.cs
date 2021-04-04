using System;
using System.Threading.Tasks;
using Users.Domain;

namespace Users.App.Repositories
{
   public interface IRefreshTokenRepository
   {
      Task<RefreshToken> GetRefreshTokensAsync(Func<RefreshToken, bool> filter);
   }
}
