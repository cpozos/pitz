using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pitz.App.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppLayer = Pitz.App;
using InfLayer = Pitz.Infraestructure;


namespace Pitz.WebAPI
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
         services.AddScoped<AppLayer.Repositories.IPersonRepository, InfLayer.PersonRepository>();
         services.AddScoped<AppLayer.Repositories.IOrganizationRepository, InfLayer.OrganizationRepository>();

         // AddIdentity registers the services
         services.AddIdentity<CustomIdentityUser, IdentityRole>(config =>
            {
               // Remove restrictions
               config.Password.RequiredLength = 4;
               config.Password.RequireDigit = false;
               config.Password.RequireNonAlphanumeric = false;
               config.Password.RequireUppercase = false;
               config.SignIn.RequireConfirmedEmail = true;
            })
            .AddUserManager<CustomUserManager>()
            .AddDefaultTokenProviders();


         //MediatR
         services.AddMediatR(typeof(AppLayer.Persons.GetPersonQuery));
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
            config.WithOrigins("https://localhost:5001");
            config.AllowAnyMethod();
            config.AllowAnyHeader();
         });

         app.UseAuthentication();

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
