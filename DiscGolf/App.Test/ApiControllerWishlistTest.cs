using App.BLL;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain.Identity;
using App.DTO.v1_0;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using WebApp.ApiControllers;
using Xunit.Abstractions;
using Wishlist = App.BLL.DTO.Wishlist;

namespace App.Test;

public class ApiControllerWishlistTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private AppDbContext _ctx;

    private IAppBLL _bll;

    private IAppUnitOfWork _uow;

    private UserManager<AppUser> _userManager;

    // sut
    private WishlistController _controller;

    public ApiControllerWishlistTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _ctx = new AppDbContext(optionsBuilder.Options);

        var configUow =
            new MapperConfiguration(cfg => cfg.CreateMap<App.Domain.Wishlist, DAL.DTO.Wishlist>().ReverseMap());
        var mapperUow = configUow.CreateMapper();


        _uow = new AppUOW(_ctx, mapperUow);

        var configBll = new MapperConfiguration(cfg => cfg.CreateMap<DAL.DTO.Wishlist, BLL.DTO.Wishlist>().ReverseMap());
        var mapperBll = configBll.CreateMapper();
        _bll = new AppBLL(_uow, mapperBll);

        var configWeb = new MapperConfiguration(cfg => cfg.CreateMap<BLL.DTO.Wishlist, DTO.v1_0.Wishlist>().ReverseMap());
        var mapperWeb = configWeb.CreateMapper();


        var storeStub = Substitute.For<IUserStore<AppUser>>();
        var optionsStub = Substitute.For<IOptions<IdentityOptions>>();
        var hasherStub = Substitute.For<IPasswordHasher<AppUser>>();

        var validatorStub = Substitute.For<IEnumerable<IUserValidator<AppUser>>>();
        var passwordStub = Substitute.For<IEnumerable<IPasswordValidator<AppUser>>>();
        var lookupStub = Substitute.For<ILookupNormalizer>();
        var errorStub = Substitute.For<IdentityErrorDescriber>();
        var serviceStub = Substitute.For<IServiceProvider>();
        var loggerStub = Substitute.For<ILogger<UserManager<AppUser>>>();

        _userManager = Substitute.For<UserManager<AppUser>>(
            storeStub, optionsStub, hasherStub,
                                validatorStub, passwordStub, lookupStub,
                                errorStub, serviceStub, loggerStub
        );

        
        _controller = new WishlistController(_ctx, _bll, _userManager, mapperWeb);
        _userManager.GetUserId(_controller.User).Returns(Guid.NewGuid().ToString());

    }

    [Fact]
    public async Task GetTest()
    {
        var result = await _controller.GetWishlist();
        var okRes = result.Result as OkObjectResult;
        var val = okRes!.Value as List<App.DTO.v1_0.Wishlist>;
        Assert.Empty(val);
    }
}
