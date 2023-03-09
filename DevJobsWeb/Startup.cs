
using Entities;
using Contracts;
using Microsoft.AspNetCore.Builder;
using Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DevJobsWeb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using DevJobsWeb.Areas.Identity.Data;

namespace DevJobsWeb
{
    public  class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {


            // var connectionString = Configuration["mysqlconnection:connectionString"];
            // services.AddDbContext<JobsOnLineContext>(o => o.UseSqlServer(connectionString));

            ////services.AddIdentity<IdentityUser, IdentityRole>()
            // //.AddEntityFrameworkStores<JobsOnLineContext>();

            // services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            // services.AddControllersWithViews();
            // services.AddControllersWithViews(config =>
            // {
            //     var policy = new AuthorizationPolicyBuilder()
            //             .RequireAuthenticatedUser()
            //             .Build();
            //     config.Filters.Add(new AuthorizeFilter(policy));
            //     config.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            // });
            // services.AddRazorPages();

            var connectionString = Configuration["mysqlconnection:connectionString"];
            services.AddDbContext<JobsOnLineContext>(o => o.UseSqlServer(connectionString));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

            });

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddControllersWithViews();


            services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromDays(1);
                o.SlidingExpiration = true;
            });


            services.Configure<DataProtectionTokenProviderOptions>(o =>
               o.TokenLifespan = TimeSpan.FromHours(3));

            services.AddControllersWithViews(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                  .RequireAuthenticatedUser()
                  .Build();
            });

            services.AddRazorPages();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          //  app.UseCors(x => x
          //.AllowAnyOrigin()
          //.AllowAnyMethod()
          //.AllowAnyHeader());

          //  app.UseHttpsRedirection();
          //  app.UseStaticFiles();
          //  app.UseStatusCodePages();

          //  app.UseRouting();
          //  app.UseAuthentication();
           
          //  app.UseAuthorization();

          //   app.UseEndpoints(endpoints =>
          //  {
          //      endpoints.MapControllerRoute(
          //          name: "default",
          //          pattern: "{controller=Home}/{action=Index}/{id?}");
          //      endpoints.MapRazorPages();
          //  });








            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages();


            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home";
                    await next();
                }
            });

           
        }


    }
}
