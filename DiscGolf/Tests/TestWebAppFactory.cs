using App.DAL.EF;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tests;

public class TestWebAppFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            var ctxSd = services.SingleOrDefault(sd => sd.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (ctxSd != null)
            {
                services.Remove(ctxSd);
            }

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("IntegrationTestsDb"));

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<TestWebAppFactory<TStartup>>>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.SaveChanges();
        });
    }
}