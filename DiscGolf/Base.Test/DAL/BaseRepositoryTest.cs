using AutoMapper;
using Base.DAL.EF;
using Base.Test.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Base.Test.DAL;

public class BaseRepositoryTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly TestDbContext _ctx;
    private readonly TestEntityRepository _testEntityRepository;

    public BaseRepositoryTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new TestDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        var config = new MapperConfiguration(cfg => cfg.CreateMap<TestEntity, TestEntity>());
        var mapper = config.CreateMapper();

        _testEntityRepository =
            new TestEntityRepository(
                _ctx,
                new BaseDalDomainMapper<TestEntity, TestEntity>(mapper)
            );
    }


    [Fact]
    public async Task TestAdd()
    {

        // arrange
        var entity = new TestEntity { Value = "Foo" };
        _testEntityRepository.Add(entity);
        await _ctx.SaveChangesAsync();

        // act
        var data = await _testEntityRepository.GetAllAsync();

        // assert
        Assert.Single(data);

    }
}