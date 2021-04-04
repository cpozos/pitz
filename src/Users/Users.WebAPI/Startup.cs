using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Users.App;
using Users.App.Repositories;
using Users.Infraestructure;

namespace Users.WebAPI
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         // JWT Settings
         var jwtService = new JwtSettings();
         Configuration.Bind(nameof(jwtService), jwtService);
         services.AddSingleton(jwtService);

         services.AddScoped<IUserRepository, UserRepository>();


         var tokenValidationParameters = new TokenValidationParameters
         {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((string)jwtService.Secret)),
            ValidateAudience = true,
            ValidAudience = jwtService.Audience,
            ValidateIssuer = true,
            ValidIssuer = jwtService.Issuer,
         };

         services.AddSingleton(tokenValidationParameters);

         services.AddAuthentication(options =>
            {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = tokenValidationParameters;
            });

         services.AddAuthorization(options =>
         {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .Build();
    
           var scopes = new[] {
             "read:users",
             "read:tournaments",
           };

           Array.ForEach(scopes, scope =>
             options.AddPolicy(scope,
               policy => policy.Requirements.Add(
                 new ScopeRequirement(jwtService.Issuer, scope)
               )
             )
           );
         });

         // Authorization handlers.
         services.AddScoped<IAuthorizationHandler, CustomAuthorizationHandler>();
         services.AddControllers();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseCors(config =>
         {
            config.AllowAnyOrigin();
            config.AllowAnyHeader();
            config.AllowAnyMethod();
         });

         app.UseAuthentication();

         app.UseRouting();
         
         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
