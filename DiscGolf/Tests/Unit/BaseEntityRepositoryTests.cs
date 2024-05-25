using Xunit;
using App.Domain; // Assuming Manufacturer is defined here
using System.Threading.Tasks;
using System.Linq;
using App.DAL.EF;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace Tests.Unit
{
    public class BaseEntityRepositoryTests
    {
        private readonly AppDbContext _ctx;
        private readonly BaseEntityRepository<Guid, Manufacturer, Manufacturer, AppDbContext> _repository;

        public BaseEntityRepositoryTests()
        {
            // Set up mock database - in-memory
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionsBuilder.Options);

            // Reset db
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Manufacturer, Manufacturer>());
            var mapper = config.CreateMapper();
            var dalMapper = new DalDomainMapper<Manufacturer, Manufacturer>(mapper);
            // Initialize repository
            _repository = new BaseEntityRepository<Guid, Manufacturer, Manufacturer, AppDbContext>(_ctx, dalMapper); // Pass appropriate mapper if needed
        }

        [Fact]
        public async Task Test_Add()
        {
            // Arrange
            var manufacturer = new Manufacturer
            {
                Id = Guid.NewGuid(),
                ManufacturerName = "Test name",
                Location = "Test location"
            };

            // Act
            var addedManufacturer = _repository.Add(manufacturer);
            await _ctx.SaveChangesAsync();

            // Assert
            Assert.NotNull(addedManufacturer);
            Assert.Equal(manufacturer.ManufacturerName, addedManufacturer.ManufacturerName);
            Assert.Equal(manufacturer.Location, addedManufacturer.Location);
        }
        [Fact]
        public async Task Test_GetAllAsync()
        {
            // Arrange
            var manufacturer = new Manufacturer
            {
                Id = Guid.NewGuid(),
                ManufacturerName = "Test name",
                Location = "Test location"
            };
            var manufacturer2 = new Manufacturer
            {
                Id = Guid.NewGuid(),
                ManufacturerName = "Test name2",
                Location = "Test location2"
            };
            _repository.Add(manufacturer);
            _repository.Add(manufacturer2);
            await _ctx.SaveChangesAsync();
            // Act
            var res =(await  _repository.GetAllAsync()).ToList();
            var secondManufacturerName = res[1].ManufacturerName;
            
            // Assert
            Assert.Equal(2,res.Count);
            Assert.Equal("Test name2", secondManufacturerName);
        }
        
        [Fact]
        public async Task Test_ExistsAsync()
        {
            // Arrange
            var manufacturerId = Guid.NewGuid();
            var manufacturer = new Manufacturer
            {
                Id = manufacturerId,
                ManufacturerName = "Test name",
                Location = "Test location"
            };
           
            _repository.Add(manufacturer);
            await _ctx.SaveChangesAsync();
            // Act
            var res =await  _repository.ExistsAsync(manufacturerId);
            
            // Assert
            Assert.True(res);

        }
        
        [Fact]
        public async Task Test_FirstOrDefaultAsync()
        {
            // Arrange
            var manufacturerId = Guid.NewGuid();
            var manufacturer1 = new Manufacturer
            {
                Id = manufacturerId,
                ManufacturerName = "Test name",
                Location = "Test location"
            };
            var manufacturer2 = new Manufacturer
            {
                Id = Guid.NewGuid(),
                ManufacturerName = "Test name2",
                Location = "Test location2"
            };
            _repository.Add(manufacturer1);
            _repository.Add(manufacturer2);
            await _ctx.SaveChangesAsync();
            // Act
            var manufacturer =await  _repository.FirstOrDefaultAsync(manufacturerId);
            
            // Assert
            Assert.NotNull(manufacturer);
            Assert.Equal(manufacturerId,manufacturer.Id);
            
        }
        
       /* [Fact]
        public async Task Test_RemoveAsync()
        {
            // Arrange
            var manufacturerId = Guid.NewGuid();
            var manufacturer1 = new Manufacturer
            {
                Id = manufacturerId,
                ManufacturerName = "Test name",
                Location = "Test location"
            };
            var manufacturer2 = new Manufacturer
            {
                Id = Guid.NewGuid(),
                ManufacturerName = "Test name2",
                Location = "Test location2"
            };
            _repository.Add(manufacturer1);
            _repository.Add(manufacturer2);
            await _ctx.SaveChangesAsync();
            
            // Act
            var manufacturers = (await _repository.GetAllAsync()).ToList();
            
            // Assert
            Assert.Equal(2,manufacturers.Count);
            
            // Act
            var manufacturer = manufacturers[0];
            await _repository.RemoveAsync(manufacturer);
            await _ctx.SaveChangesAsync();
            var manufacturersDeleted = await _repository.GetAllAsync();
            // Assert
            Assert.Single(manufacturersDeleted);
        }*/
        
    }
}