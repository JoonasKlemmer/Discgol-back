using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<AppDbContext>, IAppBLL
{
    private readonly IMapper _mapper;
    private readonly IAppUnitOfWork _uow;

    public AppBLL(IAppUnitOfWork uoW, IMapper mapper) : base(uoW)
    {
        _mapper = mapper;
        _uow = uoW;
    }

    private ICategoryService? _categories;
    public ICategoryService Categories => _categories ?? new CategoryService(_uow, _uow.Categories, _mapper);
    
    
    private IDiscService? _discs;
    public IDiscService Discs => _discs ?? new DiscService(_uow, _uow.Discs, _mapper);
    
    private IDiscFromPageService? _discsFromPages;
    public IDiscFromPageService DiscFromPage =>
        _discsFromPages ?? new DiscFromPageService(_uow, _uow.DiscFromPages, _mapper);
    
    
    private IDiscsInWishlistService? _discsInWishlists;
    public IDiscsInWishlistService DiscsInWishlists =>
        _discsInWishlists ?? new DiscsInWishlistService(_uow, _uow.DiscsInWishlists, _mapper);
    
    private IManufacturerService? _manufacturers;
    public IManufacturerService Manufacturers =>
        _manufacturers ?? new ManufacturerService(_uow, _uow.Manufacturers, _mapper);
    
    private IPriceService? _prices;
    public IPriceService Prices =>
        _prices ?? new PriceService(_uow, _uow.Prices, _mapper);
    
    private IWebsiteService? _websites;
    public IWebsiteService Websites =>
        _websites ?? new WebsiteService(_uow, _uow.Websites, _mapper);
    
    private IWishlistService? _wishlists;
    public IWishlistService Wishlists =>
        _wishlists ?? new WishlistService(_uow, _uow.Wishlists, _mapper);
    
    
    
    
    
}