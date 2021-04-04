using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Users.App.Contracts;
using Users.App.Repositories;
using Users.Domain;

namespace Users.App
{
   public class JwtService
   {
      private readonly JwtSettings _settings;
      private readonly TokenValidationParameters _tokenValidationParameters;
      private readonly IRefreshTokenRepository _refreshTokenRepository;
      private readonly IUserRepository _userRepository;
      

      public JwtService(JwtSettings settings, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, TokenValidationParameters tokenValidationParameters)
      {
         _settings = settings;
         _tokenValidationParameters = tokenValidationParameters;
         _refreshTokenRepository = refreshTokenRepository;
         _userRepository = userRepository;
      }

      public DataResponse<AuthenticationResponse> Generate(int id, string name, string email)
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

            var securityKey = new SymmetricSecurityKey(_settings.KeyBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;
            var credentials = new SigningCredentials(securityKey, algorithm);

            var token = new JwtSecurityToken(
               _settings.Issuer,
               _settings.Audience,
               claims,
               notBefore: DateTime.UtcNow,
               expires: DateTime.UtcNow.AddHours(2),
               credentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            //TODO: check if token could be a key
            var refreshToken = new RefreshToken
            {
               JwtId = token.Id,
               UserId = id,
               CreationDate = DateTime.UtcNow,
               ExpiryDate = DateTime.UtcNow.AddMonths(6),
               Token = Guid.NewGuid().ToString()
            };

            //TODO save refreshtoken on the database

            return new DataResponse<AuthenticationResponse>(true,  new AuthenticationResponse(tokenJson, refreshToken.Token));
         }
         catch
         {
            return null;
         }
      }

      public async Task<Response> ValidateRefreshTokenAsync(string currentToken, string refreshToken)
      {
         var principal = GetClaimsPrincipal(currentToken);

         if (principal is null ||
            !long.TryParse(GetClaimValue(principal.Claims, ClaimTypes.Expiration), out long expiryDateUnix))
         {
            return new Response(false, "Invalid token");
         }

         var expiryDatUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(expiryDateUnix);

         if (expiryDatUtc > DateTime.UtcNow)
         {
            return new Response(false, "This token has not expired yet");
         }

         var jti = GetClaimValue(principal.Claims, JwtRegisteredClaimNames.Jti);

         if (jti is null)
         {
            return new Response(false, "Invalid token");
         }

         var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokensAsync(token => token.Token == refreshToken);

         if (storedRefreshToken is null)
         {
            return new Response(false, "This refresh token does not exist");
         }

         if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
         {
            return new Response(false, "This refresh token has expired");
         }

         if (storedRefreshToken.Invalidated)
         {
            return new Response(false, "This refresh token has been invalidated");
         }

         if (storedRefreshToken.Used)
         {
            return new Response(false, "This refresh token has been used");
         }

         if (storedRefreshToken.JwtId != jti)
         {
            return new Response(false, "This refresh does not match this JWT");
         }


         // update status and save in database
         storedRefreshToken.Used = true;

         return new Response(true);
      }

      public Task<string> GetClaimValueAsync(string token, string claimType)
      {
         return Task.Run(() =>
         {
            var principal = GetClaimsPrincipal(token);

            if (principal is null)
               return null;

            var value = GetClaimValue(principal.Claims, claimType);
            return value;
         });
      }

      private ClaimsPrincipal GetClaimsPrincipal(string token)
      {
         var tokenHandler = new JwtSecurityTokenHandler();
         try
         {
            var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out SecurityToken validatedToken);

            if (!IsJwtTokenWithValidAlgorithm(validatedToken))
            {
               return null;
            }

            return principal;
         }
         catch
         {
            return null;
         }
      }

      private bool IsJwtTokenWithValidAlgorithm(SecurityToken validatedToken)
      {
         return
            (validatedToken is JwtSecurityToken jwtSecurityToken) &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
      }

      private string GetClaimValue(IEnumerable<Claim> claims, string claimType)
      {
         try
         {
            return claims?.Single(claim => claim.Type == claimType).Value;
         }
         catch
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
               IssuerSigningKey = new SymmetricSecurityKey(_settings.KeyBytes),
               ValidateIssuerSigningKey = true,
               ValidateAudience = false,
               ValidateIssuer = false
            }, out SecurityToken validatedToken);

         return validatedToken as JwtSecurityToken;
      }
   }
}
