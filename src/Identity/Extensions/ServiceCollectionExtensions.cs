using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using Identity.Api;
using Identity.Api.Data;
using Identity.Api.Models;
using Identity.Api.Services;
using Identity.Api.Infrastructure.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("Default");
            string assembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connString, sql => sql.MigrationsAssembly(assembly)));

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddScoped<IAccountService<ApplicationUser>, AccountService>();
            services.AddScoped<ITokenFactory, TokenFactory>();


            return services;
        }

        public static IServiceCollection AddCustomIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("Default");
            string assembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentity<ApplicationUser, ApplicationRole>(opts =>
            {

            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.UserInteraction.LoginUrl = "/Account/Login";
                options.UserInteraction.LogoutUrl = "/Account/Logout";
                options.Authentication = new AuthenticationOptions()
                {
                    CookieLifetime = TimeSpan.FromHours(24),
                    CookieSlidingExpiration = true
                };
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connString,
                    sql =>
                    {
                        sql.MigrationsAssembly(assembly);
                        sql.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connString,
                   sql =>
                   {
                       sql.MigrationsAssembly(assembly);
                       sql.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                   });
                options.EnableTokenCleanup = true;
            })
            .AddAspNetIdentity<ApplicationUser>();

            return services;
        }

    }
}
