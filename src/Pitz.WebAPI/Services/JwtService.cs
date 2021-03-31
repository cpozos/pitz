using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Pitz.WebAPI
{
   public class JwtService
   {
      public const string Issuer = "http://localhost:63154";
      public const string Audiance = "http://localhost:3000/";
      public const string _securedKey = "not_too_short_secret";
      private readonly byte[] _keyBytes = Encoding.UTF8.GetBytes(_securedKey);

      public string Generate(int id, string name, string email)
      {
         try
         {
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
               new Claim(JwtRegisteredClaimNames.Email, email),
               new Claim(JwtRegisteredClaimNames.Name, name),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(_keyBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;
            var credentials = new SigningCredentials(securityKey, algorithm);

            var token = new JwtSecurityToken(
               Issuer,
               Audiance,
               claims,
               notBefore: DateTime.Now,
               expires: DateTime.Now.AddHours(2),
               credentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenJson;
         }
         catch (Exception e)
         {
            return null;
         }
      }

      // no longer used because authorization uses authentication verification (setup)
      public JwtSecurityToken Verify(string jwt)
      {
         var tokenHandler = new JwtSecurityTokenHandler()
            .ValidateToken(jwt, new TokenValidationParameters
            {
               IssuerSigningKey = new SymmetricSecurityKey(_keyBytes),
               ValidateIssuerSigningKey = true,
               ValidateAudience = false,
               ValidateIssuer = false
            }, out SecurityToken validatedToken);

         return validatedToken as JwtSecurityToken;
      }
   }
}
