using App.DAL.EF;
using App.Domain;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace Base.Test.Unit
{
    public class BaseEntityRepositoryTests
    {
        private readonly AppDbContext _ctx;
        private readonly BaseEntityRepository<Guid, Manufacturer, App.DAL.DTO.Manufacturer, AppDbContext> _repository;
        private readonly DalDomainMapper<Manufacturer, App.DAL.DTO.Manufacturer> _dalDomainMapper;

        public BaseEntityRepositoryTests()
        {

            // Set up mock database - in-memory
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionsBuilder.Options);

            // Reset db
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            
            // Make repository
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Manufacturer, App.DAL.DTO.Manufacturer>().ReverseMap());
            var mapper = config.CreateMapper();
            _dalDomainMapper = new DalDomainMapper<Manufacturer, App.DAL.DTO.Manufacturer>(mapper);
            _repository = new BaseEntityRepository<Guid, Manufacturer, App.DAL.DTO.Manufacturer, AppDbContext>(_ctx, _dalDomainMapper);
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
            var addedManufacturer = _repository.Add(_dalDomainMapper.Map(manufacturer)!);
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
            _repository.Add(_dalDomainMapper.Map(manufacturer)!);
            _repository.Add(_dalDomainMapper.Map(manufacturer2)!);
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
           
            _repository.Add(_dalDomainMapper.Map(manufacturer)!);
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
            _repository.Add(_dalDomainMapper.Map(manufacturer1)!);
            _repository.Add(_dalDomainMapper.Map(manufacturer2)!);
            await _ctx.SaveChangesAsync();
            // Act
            var manufacturer =await  _repository.FirstOrDefaultAsync(manufacturerId);
            
            // Assert
            Assert.NotNull(manufacturer);
            Assert.Equal(manufacturerId,manufacturer.Id);
            
        }
        
        [Fact]
        public async Task Test_Update()
        {
            // Arrange
            var manufacturer = new Manufacturer
            {
                Id = Guid.NewGuid(),
                ManufacturerName = "Test name",
                Location = "Test location"
            };

            // Act
            var addedManufacturer = _repository.Add(_dalDomainMapper.Map(manufacturer)!);
            await _ctx.SaveChangesAsync();
            _ctx.ChangeTracker.Clear();

            addedManufacturer.ManufacturerName = "Updated name";
            addedManufacturer.Location = "Updated location";


            _repository.Update(addedManufacturer);
            await _ctx.SaveChangesAsync();
            var updatedManufacturer = await _ctx.Manufacturer.FindAsync(addedManufacturer.Id);
            
            
            // Assert
            Assert.NotNull(updatedManufacturer);
            Assert.Equal("Updated name", updatedManufacturer.ManufacturerName);
            Assert.Equal("Updated location", updatedManufacturer.Location);
        }
        
        
        [Fact]
        public async Task Test_RemoveAsyncByEntity()
        {
            // Arrange
            var manufacturer1 = new Manufacturer
            {
                Id = Guid.NewGuid(),
                ManufacturerName = "Test name",
                Location = "Test location"
            };
            var addedManufacturer = _repository.Add(_dalDomainMapper.Map(manufacturer1)!);
            await _ctx.SaveChangesAsync();
            _ctx.ChangeTracker.Clear();

            // Assert initial state
            Assert.Single(await _repository.GetAllAsync());

            // Act
            await _repository.RemoveAsync(addedManufacturer);
            await _ctx.SaveChangesAsync();

            // Assert final state
            var manufacturersDeleted = await _repository.GetAllAsync();
            Assert.Empty(manufacturersDeleted);
        }
        
        [Fact]
        public async Task Test_RemoveAsyncById()
        {
            // Arrange
            var manufacturerId = Guid.NewGuid();
            var manufacturer1 = new Manufacturer
            {
                Id = manufacturerId,
                ManufacturerName = "Test name",
                Location = "Test location"
            };
            var addedManufacturer = _repository.Add(_dalDomainMapper.Map(manufacturer1)!);
            await _ctx.SaveChangesAsync();
            _ctx.ChangeTracker.Clear();

            // Assert initial state
            Assert.Single(await _repository.GetAllAsync());

            // Act
            await _repository.RemoveAsync(manufacturerId);
            await _ctx.SaveChangesAsync();

            // Assert final state
            var manufacturersDeleted = await _repository.GetAllAsync();
            Assert.Empty(manufacturersDeleted);
        }
        
    }
}