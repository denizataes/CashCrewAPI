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

namespace CashCrewAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigurePostgresContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("PostgreSQLConnection");
                var builder = new NpgsqlConnectionStringBuilder(connectionString)
                {
                    // PostgreSQL özel bağlantı ayarlarını burada yapabilirsiniz
                    // Örneğin, SSL etkinleştirme:
                    // SslMode = SslMode.Require
                };

                options.UseNpgsql(builder.ToString());
            });

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
    }
}

