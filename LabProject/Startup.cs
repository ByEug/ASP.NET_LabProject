using LabProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IO;

namespace LabProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<DataContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddDbContextPool<CartContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddDbContext<UserDataContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<EmailService>();
            services.AddSingleton<CustomLogger>();

            services.AddSignalR();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            { 
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Home/Error?code={0}");
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<RolesHub>("/RolesHub");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
