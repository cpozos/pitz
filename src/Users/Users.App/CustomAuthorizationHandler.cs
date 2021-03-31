using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Users.App
{
   public class CustomAuthorizationHandler : AuthorizationHandler<ScopeRequirement>
   {
      protected override Task HandleRequirementAsync(
         AuthorizationHandlerContext context, 
         ScopeRequirement requirement)
      {
         // The scope must have originated from our issuer and must have the email
         var scopeClaim = context.User.FindFirst(c => c.Type == ClaimTypes.Email && c.Issuer == requirement.Issuer);
         if (scopeClaim == null || string.IsNullOrEmpty(scopeClaim.Value))
            return Task.CompletedTask;

         // A token can contain multiple scopes and we need at least one exact match.
         if (scopeClaim.Value.Split(' ').Any(s => s == requirement.Scope))
            context.Succeed(requirement);

         return Task.CompletedTask;
      }
   }
}
