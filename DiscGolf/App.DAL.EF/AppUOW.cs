using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Repositories;
using App.Domain.Identity;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    private readonly IMapper _mapper;
    public AppUOW(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }


    private ICategoryRepository? _categories;
    public ICategoryRepository Categories => _categories ?? new CategoryRepository(UowDbContext, _mapper);
    
    
    private IDiscRepository? _discs;
    public IDiscRepository Discs => _discs ?? new DiscRepository(UowDbContext, _mapper);
    
    private IDiscFromPageRepository? _discFromPages;
    public IDiscFromPageRepository DiscFromPages => _discFromPages ?? new DiscFromPagesRepository(UowDbContext, _mapper);
    
    private IDiscsInWishlistRepository? _discsInWishlists;
    public IDiscsInWishlistRepository DiscsInWishlists => _discsInWishlists ?? new DiscsInWishlistRepository(UowDbContext, _mapper);


    private IPriceRepository? _prices;
    public IPriceRepository Prices => _prices ?? new PriceRepository(UowDbContext, _mapper);
    
    private IWebsiteRepository? _websites;
    public IWebsiteRepository Websites => _websites ?? new WebsiteRepository(UowDbContext, _mapper);
    
    private IManufacturerRepository? _manufacturers;
    public IManufacturerRepository Manufacturers => _manufacturers ?? new ManufacturerRepository(UowDbContext, _mapper);
    

    private IWishlistRepository? _wishlists;
    public IWishlistRepository Wishlists => _wishlists ?? new WishlistRepository(UowDbContext, _mapper);

    private IEntityRepository<AppUser>? _users;
    public IEntityRepository<AppUser> Users => _users ??
                                               new BaseEntityRepository<AppUser, AppUser, AppDbContext>(UowDbContext,
                                                   new DalDomainMapper<AppUser, AppUser>(_mapper));
}