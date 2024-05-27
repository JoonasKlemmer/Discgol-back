using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace App.Test.Unit
{
    public class DiscFromPageRepositoryTests
    {
        private readonly AppDbContext _ctx;
        private readonly DiscFromPagesRepository _discFromPagesRepository;
        private readonly IMapper _mapper;

       
        private readonly Guid _categoryDistanceId = Guid.NewGuid();
        private readonly Guid _categoryPutterId = Guid.NewGuid();
        private readonly Guid _manufacturerInnovaId = Guid.NewGuid();
        private readonly Guid _manufacturerDiscraftId = Guid.NewGuid();
        private readonly Guid _manufacturerDisctroyerId = Guid.NewGuid();
        private readonly Guid _destroyerId = Guid.NewGuid();
        private readonly Guid _nightJarId = Guid.NewGuid();
        private readonly Guid _lunaId = Guid.NewGuid();
        private readonly Guid _luna2Id = Guid.NewGuid();
        private readonly Guid _destroyerWebsiteId = Guid.NewGuid();
        private readonly Guid _nightJarWebsiteId = Guid.NewGuid();
        private readonly Guid _euroPriceId = Guid.NewGuid();
        private readonly Guid _lunaWebsiteId = Guid.NewGuid();
        private readonly Guid _luna2WebsiteId = Guid.NewGuid();
        private readonly Guid _destroyerFromDiscgolfPageId = Guid.NewGuid();
        private readonly Guid _nightJarFromDisctroyerPageId = Guid.NewGuid();
        private readonly Guid _lunaFromDiscraftPageId = Guid.NewGuid();
        private readonly Guid _lunaFromDiscsportPageId = Guid.NewGuid();
        
        public DiscFromPageRepositoryTests()
        {

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            _ctx = new AppDbContext(optionsBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();

            _discFromPagesRepository = new DiscFromPagesRepository(_ctx, _mapper);
            SeedTheData();
            
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

        }
        
        [Fact]
        public async Task Test_GetAllSortedAsync()
        {
            //arrange
            var sortedDiscs = (await _discFromPagesRepository.GetAllSortedAsync()).ToList();
            var cheapestDisc = sortedDiscs[0].Price;
            var mostExpensiveDisc = sortedDiscs[sortedDiscs.Count() - 1 ].Price;
            
            //act
            //assert
            Assert.True(cheapestDisc < mostExpensiveDisc);

        }
        
        [Fact]
        public async Task Test_GetAllWithDetails()
        {
            //arrange
            var discsWithDetails = (await _discFromPagesRepository.GetAllWithDetails()).ToList();
            //act
            //assert
            Assert.NotNull(discsWithDetails[0].Discs!.Manufacturers!.ManufacturerName);
        }
        
        [Fact]
        public async Task Test_GetAllWithDetailsByName()
        {
            //arrange
            var discName = "Luna"; //disc added with seeding
            var discsWithDetails = (await _discFromPagesRepository.GetAllWithDetailsByName(discName)).ToList();
            
            //act
            //assert
            Assert.All(discsWithDetails, discWithDetails =>
            {
                Assert.NotNull(discWithDetails);
                Assert.NotNull(discWithDetails.Discs);
                Assert.Equal(discName, discWithDetails.Discs.Name);
            });
        }
        
        
        [Fact]
        public async Task Test_GetAllWithDetailsById()
        {
            //arrange
            var discsWithDetails = (await _discFromPagesRepository.GetWithDetailsByDiscId(_destroyerId)).ToList();
            //act
            //assert
            Assert.Single(discsWithDetails);
        }
        
        private void SeedTheData()
        {
            SeedCategories();
            SeedManufacturers();
            SeedWebsite();
            SeedPrice();
            SeedDiscs();
            SeedDiscFromPage();
        }

        private void SeedCategories()
        {
            var distanceCategory = new Category()
            {
                Id = _categoryDistanceId,
                CategoryName = "Distance",
            };

            var putterCategory = new Category()
            {
                Id = _categoryPutterId,
                CategoryName = "Putter",
            };

            _ctx.Category.Add(distanceCategory);
            _ctx.Category.Add(putterCategory);
            _ctx.SaveChanges();
        }

        private void SeedManufacturers()
        {
            var innovaManufacturer = new Manufacturer
            {
                Id = _manufacturerInnovaId,
                ManufacturerName = "Innova",
                Location = "USA"
            };
            var discraftManufacturer = new Manufacturer
            {
                Id = _manufacturerDiscraftId,
                ManufacturerName = "Discraft",
                Location = "USA"
            };
            var disctroyerManufacturer = new Manufacturer
            {
                Id = _manufacturerDisctroyerId,
                ManufacturerName = "Disctroyer",
                Location = "Estonia"
            };

            _ctx.Manufacturer.Add(innovaManufacturer);
            _ctx.Manufacturer.Add(discraftManufacturer);
            _ctx.Manufacturer.Add(disctroyerManufacturer);
            _ctx.SaveChanges();
        }

        private void SeedDiscs()
        {
            var destroyer = new Disc()
            {
                Id = _destroyerId,
                Name = "Destroyer",
                Speed = 12,
                Glide = 5,
                Turn = -1,
                Fade = 3,
                ManufacturerId = _manufacturerInnovaId,
                CategoryId = _categoryDistanceId
            };
            var nightJar = new Disc()
            {
                Id = _nightJarId,
                Name = "Night Jar",
                Speed = 10,
                Glide = 5,
                Turn = -0.5,
                Fade = 2.5,
                ManufacturerId = _manufacturerDisctroyerId,
                CategoryId = _categoryDistanceId
            };
            var luna = new Disc()
            {
                Id = _lunaId,
                Name = "Luna",
                Speed = 3,
                Glide = 3,
                Turn = 0,
                Fade = 3,
                ManufacturerId = _manufacturerDiscraftId,
                CategoryId = _categoryPutterId
            };

            _ctx.Disc.Add(destroyer);
            _ctx.Disc.Add(nightJar);
            _ctx.Disc.Add(luna);
            _ctx.SaveChanges();
        }

        private void SeedWebsite()
        {
            var destroyerWebsite = new Website()
            {
                Id = _destroyerWebsiteId,
                Url = "https://discgolf.ee/collections/koik-tooted/products/innova-gstar-destroyer?variant=42819763929328",
                WebsiteName = "https://discgolf.ee/cdn/shop/products/gstardestroyer.jpg?v=1652275109",
            };

            var nightJarWebsite = new Website()
            {
                Id = _nightJarWebsiteId,
                Url = "https://www.disctroyer.com/pood/nightjar-x-out",
                WebsiteName = "https://images.squarespace-cdn.com/content/v1/5c6b13128d9740541891f0ca/1662716657922-AUYAXU19ZQS9ZU3O889W/Nightjar_x-out3.png?format=2500w",
            };
            var lunaWebsite = new Website()
            {
                Id = _lunaWebsiteId,
                Url = "https://www.discraft.com/disc-golf/paul-mcbeth-luna-mcbethluna?returnurl=%2fdisc-golf%2f%3fcount%3d36",
                WebsiteName = "https://www.discraft.com/product/image/large/mcbethluna_1.jpg",
            };
            var luna2Website = new Website()
            {
                Id = _luna2WebsiteId,
                Url = "https://www.discsport.ee/et/e-pood/luhimaa-kettad/discraft-big-z-luna-paul-mcbeth-detail",
                WebsiteName = "https://www.discsport.ee/images/stories/virtuemart/product/mcbethbzluna_1.png",
            };

            _ctx.Website.Add(destroyerWebsite);
            _ctx.Website.Add(nightJarWebsite);
            _ctx.Website.Add(lunaWebsite);
            _ctx.Website.Add(luna2Website);
            _ctx.SaveChanges();
        }

        private void SeedPrice()
        {
            var euroPirce = new Price()
            {
                Id = _euroPriceId,
                Currency = "EURO",
                Iso = "EUR",
                Symbol = "€"
            };

            _ctx.Price.Add(euroPirce);
            _ctx.SaveChanges();
        }

        private void SeedDiscFromPage()
        {
            var destroyerFromDiscgolfPage = new DiscFromPage()
            {
                Id = _destroyerFromDiscgolfPageId,
                Price = 19.90,
                DiscId = _destroyerId,
                WebsiteId = _destroyerWebsiteId,
                PriceId = _euroPriceId
            };
            var nighjarFromDisctroyerPage = new DiscFromPage()
            {
                Id = _nightJarFromDisctroyerPageId,
                Price = 18.90,
                DiscId = _nightJarId,
                WebsiteId = _nightJarWebsiteId,
                PriceId = _euroPriceId
            };
            var lunaFromDiscraftPage = new DiscFromPage()
            {
                Id = _lunaFromDiscraftPageId,
                Price = 13.20,
                DiscId = _lunaId,
                WebsiteId = _lunaWebsiteId,
                PriceId = _euroPriceId
            };
            var lunaFromDiscsportPage = new DiscFromPage()
            {
                Id = _lunaFromDiscsportPageId,
                Price = 23.90,
                DiscId = _luna2Id,
                WebsiteId = _luna2WebsiteId,
                PriceId = _euroPriceId
            };
            
            _ctx.DiscsFromPage.Add(destroyerFromDiscgolfPage);
            _ctx.DiscsFromPage.Add(nighjarFromDisctroyerPage);
            _ctx.DiscsFromPage.Add(lunaFromDiscraftPage);
            _ctx.DiscsFromPage.Add(lunaFromDiscsportPage);
            _ctx.SaveChanges();
        }
    }
}
