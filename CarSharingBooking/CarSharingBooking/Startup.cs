using CarSharingBooking.CustomSetup;
using CarSharingBooking.CustomStartup;
using CarSharingBooking.Repositories.BusinessService;
using CarSharingBooking.Repositories.DBContext;
using CarSharingBooking.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking
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
            services.AddMvc(setting => setting.Filters.Add(
                 new CustomAuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build())
            ));

            services.AddScoped<CustomActionFilter>();

            services.AddControllers();

            services.AddTransient<ICarBookingRepo,Repository_Car>();
            services.AddTransient<ICarUserBookingRepo, Repository_CarUser>();
            services.AddTransient<IUserBookingRepo, Repository_User>();
            services.AddTransient<IUnitOfWork, UnitOfWorks>();
            services.AddTransient<ICarBusinessFacade, CarBusinessFacade>();

            services.AddDbContext<masterContext>(opt => opt.UseSqlServer(
                "Server=ANTONIUSRICARDO;Database=master;Trusted_Connection=True;"
                ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory factory)
        {
            app.APIConfigureExceptionHandler(factory);

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults : new { controller = "CarBooking", action = "GetCarDetails" }
                );
            });
        }
    }
}
