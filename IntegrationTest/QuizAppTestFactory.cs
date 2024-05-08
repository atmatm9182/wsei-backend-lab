using System.Data.Common;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTest;

public class QuizAppTestFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContext = services.SingleOrDefault(
                s => s.ServiceType == typeof(DbContextOptions<QuizDbContext>));
            services.Remove(dbContext);

            var dbConnection = services.SingleOrDefault(
                s => s.ServiceType == typeof(DbConnection));
            services.Remove(dbConnection);

            services.AddEntityFrameworkInMemoryDatabase().AddDbContext<QuizDbContext>((container, options) =>
            {
                options.UseInMemoryDatabase("QuizTest").UseInternalServiceProvider(container);
            });

            builder.UseEnvironment("Development");
        });
        base.ConfigureWebHost(builder);
    }
}