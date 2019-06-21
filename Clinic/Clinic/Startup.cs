using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinic.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
            {
                // Set the timeout for session values
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                // options.Cookie.Name = ".AspNetCore.Session";
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.IdleTimeout = TimeSpan.FromMinutes(1440);
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); ;
            services.AddAuthorization(options =>
            {

                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Doctor", policy => policy.RequireRole("Doctor"));
                options.AddPolicy("Patient", policy => policy.RequireRole("Patient"));
                options.AddPolicy("Assistant", policy => policy.RequireRole("Assistant"));
                options.AddPolicy("Insurance", policy => policy.RequireRole("Insurance"));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Admin",
                    template:"/Admin",
               defaults: new { controller = "Messages", action = "Index" });

               
                    

                routes.MapRoute(
                   name: "Doctor",
                   template: "/Doctor",
                   defaults: new { controller = "Doctors", action = "Index" });

                routes.MapRoute(
                        name: "Patient",
                     template: "/Patient",
                     defaults: new { controller = "Patients", action = "Index" });

                routes.MapRoute(
                  name: "Assistant",
                  template: "/Assistant",
                 defaults: new { controller = "Assistants", action = "Index" });

                routes.MapRoute(
              name: "Insurance",
              template: "/Insurance",
             defaults: new { controller = "InsuranceCompanies", action = "Index" });

                routes.MapRoute(
             name: "Home",
             template: "/home",
            defaults: new { controller = "Home", action = "Index" });


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
