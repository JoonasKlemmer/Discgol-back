using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Base.Test.Helpers;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup: class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // change the di container registrations
            
            
            // find DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppDbContext>));

            // if found - remove
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // and new DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            
            // create db and seed data
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            db.Database.EnsureCreated();
            
             var categoryDistanceId = Guid.NewGuid();
            var categoryPutterId = Guid.NewGuid();
            var manufacturerInnovaId = Guid.NewGuid();
            var manufacturerDiscraftId = Guid.NewGuid();
            var manufacturerDisctroyerId = Guid.NewGuid();
            var destroyerId = Guid.NewGuid();
            var nightJarId = Guid.NewGuid();
            var lunaId = Guid.NewGuid();
            var luna2Id = Guid.NewGuid();
            var destroyerWebsiteId = Guid.NewGuid();
            var nightJarWebsiteId = Guid.NewGuid();
            var lunaWebsiteId = Guid.NewGuid();
            var luna2WebsiteId = Guid.NewGuid();
            var euroPriceId = Guid.NewGuid();
            var destroyerFromDiscgolfPageId = Guid.NewGuid();
            var nightJarFromDisctroyerPageId = Guid.NewGuid();
            var lunaFromDiscraftPageId = Guid.NewGuid();
            var lunaFromDiscsportPageId = Guid.NewGuid();





            // Seed categories
            var distanceCategory = new Category()
            {
                Id = categoryDistanceId,
                CategoryName = "Distance",
            };

            var putterCategory = new Category()
            {
                Id = categoryPutterId,
                CategoryName = "Putter",
            };

            db.Category.Add(distanceCategory);
            db.Category.Add(putterCategory);

            // Seed manufacturers


            var innovaManufacturer = new Manufacturer
            {
                Id = manufacturerInnovaId,
                ManufacturerName = "Innova",
                Location = "USA"
            };
            var discraftManufacturer = new Manufacturer
            {
                Id = manufacturerDiscraftId,
                ManufacturerName = "Discraft",
                Location = "USA"
            };
            var disctroyerManufacturer = new Manufacturer
            {
                Id = manufacturerDisctroyerId,
                ManufacturerName = "Disctroyer",
                Location = "Estonia"
            };

            db.Manufacturer.Add(innovaManufacturer);
            db.Manufacturer.Add(discraftManufacturer);
            db.Manufacturer.Add(disctroyerManufacturer);
            db.SaveChanges();



            var destroyer = new Disc()
            {
                Id = destroyerId,
                Name = "Destroyer",
                Speed = 12,
                Glide = 5,
                Turn = -1,
                Fade = 3,
                ManufacturerId = manufacturerInnovaId,
                CategoryId = categoryDistanceId
            };
            var nightJar = new Disc()
            {
                Id = nightJarId,
                Name = "Night Jar",
                Speed = 10,
                Glide = 5,
                Turn = -0.5,
                Fade = 2.5,
                ManufacturerId = manufacturerDisctroyerId,
                CategoryId = categoryDistanceId
            };
            var luna = new Disc()
            {
                Id = lunaId,
                Name = "Luna",
                Speed = 3,
                Glide = 3,
                Turn = 0,
                Fade = 3,
                ManufacturerId = manufacturerDiscraftId,
                CategoryId = categoryPutterId
            };

            db.Disc.Add(destroyer);
            db.Disc.Add(nightJar);
            db.Disc.Add(luna);
            db.SaveChanges();


            var destroyerWebsite = new Website()
            {
                Id = destroyerWebsiteId,
                Url =
                    "https://discgolf.ee/collections/koik-tooted/products/innova-gstar-destroyer?variant=42819763929328",
                WebsiteName = "https://discgolf.ee/cdn/shop/products/gstardestroyer.jpg?v=1652275109",
            };

            var nightJarWebsite = new Website()
            {
                Id = nightJarWebsiteId,
                Url = "https://www.disctroyer.com/pood/nightjar-x-out",
                WebsiteName =
                    "https://images.squarespace-cdn.com/content/v1/5c6b13128d9740541891f0ca/1662716657922-AUYAXU19ZQS9ZU3O889W/Nightjar_x-out3.png?format=2500w",
            };
            var lunaWebsite = new Website()
            {
                Id = lunaWebsiteId,
                Url =
                    "https://www.discraft.com/disc-golf/paul-mcbeth-luna-mcbethluna?returnurl=%2fdisc-golf%2f%3fcount%3d36",
                WebsiteName = "https://www.discraft.com/product/image/large/mcbethluna_1.jpg",
            };
            var luna2Website = new Website()
            {
                Id = luna2WebsiteId,
                Url = "https://www.discsport.ee/et/e-pood/luhimaa-kettad/discraft-big-z-luna-paul-mcbeth-detail",
                WebsiteName = "https://www.discsport.ee/images/stories/virtuemart/product/mcbethbzluna_1.png",
            };

            db.Website.Add(destroyerWebsite);
            db.Website.Add(nightJarWebsite);
            db.Website.Add(lunaWebsite);
            db.Website.Add(luna2Website);
            db.SaveChanges();


            var euroPirce = new Price()
            {
                Id = euroPriceId,
                Currency = "EURO",
                Iso = "EUR",
                Symbol = "€"
            };

            db.Price.Add(euroPirce);
            db.SaveChanges();



            var destroyerFromDiscgolfPage = new DiscFromPage()
            {
                Id = destroyerFromDiscgolfPageId,
                Price = 19.90,
                DiscId = destroyerId,
                WebsiteId = destroyerWebsiteId,
                PriceId = euroPriceId
            };
            var nighjarFromDisctroyerPage = new DiscFromPage()
            {
                Id = nightJarFromDisctroyerPageId,
                Price = 18.90,
                DiscId = nightJarId,
                WebsiteId = nightJarWebsiteId,
                PriceId = euroPriceId
            };
            var lunaFromDiscraftPage = new DiscFromPage()
            {
                Id = lunaFromDiscraftPageId,
                Price = 13.20,
                DiscId = lunaId,
                WebsiteId = lunaWebsiteId,
                PriceId = euroPriceId
            };
            var lunaFromDiscsportPage = new DiscFromPage()
            {
                Id = lunaFromDiscsportPageId,
                Price = 23.90,
                DiscId = luna2Id,
                WebsiteId = luna2WebsiteId,
                PriceId = euroPriceId
            };

            db.DiscsFromPage.Add(destroyerFromDiscgolfPage);
            db.DiscsFromPage.Add(nighjarFromDisctroyerPage);
            db.DiscsFromPage.Add(lunaFromDiscraftPage);
            db.DiscsFromPage.Add(lunaFromDiscsportPage);
            db.SaveChanges(); 
        });
    }
}