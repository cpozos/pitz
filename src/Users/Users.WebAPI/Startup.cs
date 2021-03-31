using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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


         //services.AddAuthentication("OAuth")
         //   .AddJwtBearer("OAuth", config =>
         //   {
         //      var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
         //      var key = new SymmetricSecurityKey(secretBytes);

         //      config.TokenValidationParameters = new TokenValidationParameters
         //      {
         //         ValidIssuer = Constants.Issuer,
         //         ValidAudience = Constants.Audiance,
         //         IssuerSigningKey = key
         //      };
         //   });

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

         //app.UseAuthentication();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
