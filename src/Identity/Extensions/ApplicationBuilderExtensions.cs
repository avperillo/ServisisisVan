using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Identity.Api.Data;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder InitializeDatabases(this IApplicationBuilder app)
        {
            using (var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var identityContext = scopeFactory.ServiceProvider.GetRequiredService<IdentityContext>();
                identityContext.Database.Migrate();

                var persistedGrantContext = scopeFactory.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                persistedGrantContext.Database.Migrate();

                var configurationContext = scopeFactory.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                persistedGrantContext.Database.Migrate();
            }

            return app;
        }

    }
}
