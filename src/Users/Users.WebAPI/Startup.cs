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
using Users.App.Services;
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
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddScoped<JwtService>();

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
               options.TokenValidationParameters = new TokenValidationParameters
               {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtService._securedKey)),
                  ValidateAudience = true,
                  ValidAudience = JwtService.Audiance,
                  ValidateIssuer = true,
                  ValidIssuer = JwtService.Issuer,
               };
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
                 new ScopeRequirement(JwtService.Issuer, scope)
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


         app.UseRouting();

         app.UseAuthentication();
         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
