using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Travel.API.Infrastructure.Queries;
using Travel.API.Infrastructure.Services;
using Travel.Domain.AggregatesModel.CollectionAggregate;
using Travel.Domain.AggregatesModel.RefuelAggregate;
using Travel.Domain.AggregatesModel.TravelerAggregate;
using Travel.Domain.AggregatesModel.TripAggregate;
using Travel.Infrastructure;
using Travel.Infrastructure.Repositories;
using Travel.Infrastructure.Services;

namespace Travel.API
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
            services.AddCustomMvc()
                .AddCustomDbContext(Configuration)
                .AddCustomMapper()
                .AddSwagger()
                .AddRepositories()
                .AddQueries()
                .AddApplicationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    static class ServiceExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddControllersAsServices();  //Injecting Controllers themselves thru DI
                                              //For further info see: http://docs.autofac.org/en/latest/integration/aspnetcore.html#controllers-as-services

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                   .AddDbContext<TravelContext>(options =>
                   {
                       options.UseSqlServer(configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                   );

            return services;
        }

        public static IServiceCollection AddCustomMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddScoped<IMapperService, MapperService>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Travel HTTP API",
                    Version = "v1"
                });

            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<ITravelerRepository, TravelerRepository>();
            services.AddScoped<IRefuelRepository, RefuelRepository>();
            services.AddScoped<ICollectionRepository, CollectionRepository>();

            return services;
        }

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<ITripQueries, TripQueries>();
            services.AddScoped<ITravelerQueries, TravelerQueries>();
            services.AddScoped<IRefuelQueries, RefuelQueries>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITripService, TripService>();
            services.AddScoped<ITravelerService, TravelerService>();
            services.AddScoped<IRefuelService, RefuelService>();
            services.AddScoped<ICollectionService, CollectionService>();

            return services;
        }

    }

    static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddSwaggerConfigure(this IApplicationBuilder builder)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            builder.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            return builder;
        }
    }
}
