using System;
using System.Threading.Tasks;
using Pitz.Identity.Domain;

namespace Pitz.Identity.App.Repositories
{
   public interface IRefreshTokenRepository
   {
      Task<RefreshToken> GetRefreshTokensAsync(Func<RefreshToken, bool> filter);
   }
}
