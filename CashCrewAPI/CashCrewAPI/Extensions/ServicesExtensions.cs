﻿using System;
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

        public static void ConfigureMSSQLContext(this IServiceCollection services, IConfiguration configuration) =>
         services.AddDbContext<RepositoryContext>(options =>
         options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection")));


        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVacationRepository, VacationRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IVacationUserAssociationRepository, VacationUserAssociationRepository>();
            services.AddScoped<IPaymentParticipantRepository, PaymentParticipantRepository>();
            services.AddScoped<IDebtRepository, DebtRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<ILoginService, LoginManager>();
            services.AddScoped<IVacationService, VacationManager>();
            services.AddScoped<IPaymentService, PaymentManager>();
            services.AddScoped<IDebtService, DebtManager>();
        }
        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<LogFilterAttribute>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("X-Pagination"));
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services) =>
    services.AddSingleton<ILoggerService, LoggerManager>();
    }
}

