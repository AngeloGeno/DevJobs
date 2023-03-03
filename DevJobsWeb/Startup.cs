
using Entities;
using Contracts;
using Microsoft.AspNetCore.Builder;
using Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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
            

            var connectionString = Configuration["mysqlconnection:connectionString"];
            services.AddDbContext<DevJobsWeb.JobsOnLineContext>(o => o.UseSqlServer(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<JobsOnLineContext>();
            services.AddMvc(option =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                option.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddSessionStateTempDataProvider();

            //services.AddAuthorization(options =>
            //{
            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //      .RequireAuthenticatedUser()
            //      .Build();
            //});


            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddRazorPages();

        }

        public void Configure(IApplicationBuilder app)
        {
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

        }

        //public static void Configure(IApplicationBuilder app)
        //{
        //    app.UseStaticFiles();
        //    app.UseRouting();
        //    app.UseSession();
        //    app.UseAuthentication();
        //    app.UseAuthorization();
        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllerRoute(
        //            name: "default",
        //            pattern: "{controller=Home}/{action=Index}/{id?}");
        //        endpoints.MapRazorPages();
        //    });

        //    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //}
        //public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        //{
        //    services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();   
        //    services.AddRazorPages();

        //    services.AddIdentity<IdentityUser, IdentityRole>()
        //    .AddEntityFrameworkStores<JobsOnLineContext>();
        //    services.AddMvc(option =>
        //    {
        //       var policy = new AuthorizationPolicyBuilder()
        //                        .RequireAuthenticatedUser()
        //                        .Build();
        //        option.Filters.Add(new AuthorizeFilter(policy));
        //    })
        //    .AddSessionStateTempDataProvider();
        //    //services.AddAuthorization(options =>
        //    //{
        //    //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        //    //      //.RequireAuthenticatedUser()
        //    //      .Build();
        //    //});

        //}
        //public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        //{
        //    var connectionString = config["mysqlconnection:connectionString"];
        //    services.AddDbContext<DevJobsWeb.JobsOnLineContext>(o => o.UseSqlServer(connectionString));
        //}

        //public static void ConfigureIISIntegration(this IServiceCollection services)
        //{
        //    services.Configure<IISOptions>(options =>
        //    {

        //    });
        //}
    }
}
