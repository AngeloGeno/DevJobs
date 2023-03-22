using Contracts;
using DevJobsAPI;
using DevJobsAPI.Extentions;
using DevJobsWeb;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Repository;

namespace DevJobsWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });

    }

}



//var builder = WebApplication.CreateBuilder(args);

//builder.Services.ConfigureRepositoryWrapper();
//builder.Services.ConfigureMySqlContext(builder.Configuration);


// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();
//app.UseAuthentication();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
