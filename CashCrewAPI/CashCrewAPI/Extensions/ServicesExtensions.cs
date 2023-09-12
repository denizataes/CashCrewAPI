using System;
using Microsoft.AspNetCore.Authentication;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Presentation.ActionFilters;

namespace CashCrewAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigurePostgresContext(this IServiceCollection services,
            IConfiguration configuration) => services.AddDbContext<RepositoryContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>(); 
        }
        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<LogFilterAttribute>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services) =>
    services.AddSingleton<ILoggerService, LoggerManager>();
    }
}

