using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Entities.Models;


namespace DevJobsAPI.Extentions
{
    public static class ServiceExtention
    {

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<JobsOnLineContext>(o => o.UseSqlServer(connectionString));
        }
                                                                                                                                              
        public static void ConfigureCors(this IServiceCollection  services)
        {

            services.AddCors(options =>
            {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                    );
            }
            );

        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

       

        public static void ConfigureLoggerService(this IServiceCollection services)
        {

            services.AddSingleton<ILoggerManager, LoggerManager>();

        }

    }
}
