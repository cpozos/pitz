using System;
using System.Text;

namespace Users.App
{
   public class JwtSettings
   {
      public string Issuer { get; set; }
      public string Audience { get; set; }
      public string Secret { get; set; }
      public TimeSpan TokenLifetime { get; set; }

      private byte[] _keyBytes;
      public byte[] KeyBytes => _keyBytes ??= Encoding.UTF8.GetBytes(Secret);
   }
}