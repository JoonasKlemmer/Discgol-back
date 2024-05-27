using App.BLL;
using App.DAL.EF;
using App.Domain;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Xunit.Abstractions;

namespace Base.Test.Unit
{
    public class BaseEntityServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;
        private readonly DalDomainMapper<Manufacturer, App.DAL.DTO.Manufacturer> _dalDomainMapper;
        private readonly BllDalMapper<App.DAL.DTO.Manufacturer, App.BLL.DTO.Manufacturer> _bllDalMapper;
        private readonly BaseEntityService<App.DAL.DTO.Manufacturer, App.BLL.DTO.Manufacturer, BaseEntityRepository<Manufacturer, App.DAL.DTO.Manufacturer, AppDbContext>, Guid> _service;

        public BaseEntityServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            // Set up mock database - in-memory
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionsBuilder.Options);

            // Reset db
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            // Make mappers
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new App.BLL.AutoMapperProfile());
                cfg.AddProfile(new App.DAL.EF.AutoMapperProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            _dalDomainMapper = new DalDomainMapper<Manufacturer, App.DAL.DTO.Manufacturer>(mapper);
            _bllDalMapper = new BllDalMapper<App.DAL.DTO.Manufacturer, App.BLL.DTO.Manufacturer>(mapper);

            // Make repository
            BaseEntityRepository<Manufacturer, App.DAL.DTO.Manufacturer, AppDbContext> repository = new(_ctx, _dalDomainMapper);

            // Make service
            IUnitOfWork uow = new AppUOW(_ctx,mapper);
            _service = new BaseEntityService<App.DAL.DTO.Manufacturer, App.BLL.DTO.Manufacturer, BaseEntityRepository<Manufacturer, App.DAL.DTO.Manufacturer, AppDbContext>, Guid>(uow, repository, _bllDalMapper);
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
            var dalToBll = _bllDalMapper.Map(_dalDomainMapper.Map(manufacturer)); // Map domain to dal to bll
            var addedManufacturer = _service.Add(dalToBll!);
            await _ctx.SaveChangesAsync();
            _testOutputHelper.WriteLine(addedManufacturer.ToJson());
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
            _service.Add(_bllDalMapper.Map(_dalDomainMapper.Map(manufacturer))!);
            _service.Add(_bllDalMapper.Map(_dalDomainMapper.Map(manufacturer2))!);
            await _ctx.SaveChangesAsync();
            
            // Act
            var res =(await  _service.GetAllAsync()).ToList();
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
           
            _service.Add(_bllDalMapper.Map(_dalDomainMapper.Map(manufacturer))!);
            await _ctx.SaveChangesAsync();
            // Act
            // Assert
            Assert.True(await  _service.ExistsAsync(manufacturerId));

        }
        
        [Fact]
        public async Task Test_FirstOrDefaultAsync()
        {
            // Arrange
            var manufacturerId = Guid.NewGuid();
            var manufacturer = new Manufacturer
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
            _service.Add(_bllDalMapper.Map(_dalDomainMapper.Map(manufacturer))!);
            _service.Add(_bllDalMapper.Map(_dalDomainMapper.Map(manufacturer2))!);
            await _ctx.SaveChangesAsync();
            // Act
            var firstManufacturer =await  _service.FirstOrDefaultAsync(manufacturerId);
            
            // Assert
            Assert.NotNull(firstManufacturer);
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
            var addedManufacturer = _service.Add(_bllDalMapper.Map(_dalDomainMapper.Map(manufacturer))!);
            await _ctx.SaveChangesAsync();
            _ctx.ChangeTracker.Clear();

            addedManufacturer.ManufacturerName = "Updated name";
            addedManufacturer.Location = "Updated location";


            _service.Update(addedManufacturer);
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
            var manufacturer = new Manufacturer
            {
                Id = Guid.NewGuid(),
                ManufacturerName = "Test name",
                Location = "Test location"
            };
            var addedManufacturer = _service.Add(_bllDalMapper.Map(_dalDomainMapper.Map(manufacturer))!);
            await _ctx.SaveChangesAsync();
            _ctx.ChangeTracker.Clear();

            // Assert initial state
            Assert.Single(await _service.GetAllAsync());

            // Act
            await _service.RemoveAsync(addedManufacturer);
            await _ctx.SaveChangesAsync();
            var manufacturersDeleted = await _service.GetAllAsync();

            // Assert final state
            Assert.Empty(manufacturersDeleted);
        }
        
        [Fact]
        public async Task Test_RemoveAsyncById()
        {
            // Arrange
            var manufacturerId = Guid.NewGuid();
            var manufacturer = new Manufacturer
            {
                Id = manufacturerId,
                ManufacturerName = "Test name",
                Location = "Test location"
            };
            _service.Add(_bllDalMapper.Map(_dalDomainMapper.Map(manufacturer))!);
            await _ctx.SaveChangesAsync();
            _ctx.ChangeTracker.Clear();

            // Assert initial state
            Assert.Single(await _service.GetAllAsync());

            // Act
            await _service.RemoveAsync(manufacturerId);
            await _ctx.SaveChangesAsync();

            // Assert final state
            var manufacturersDeleted = await _service.GetAllAsync();
            Assert.Empty(manufacturersDeleted);
        }
    }
}