using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    
    ICategoryService Categories { get; }
    IDiscService Discs { get; }
    IDiscFromPageService DiscFromPages { get; }
    IDiscsInWishlistService DiscsInWishlists { get; }
    IManufacturerService Manufacturers { get; }
    IPriceService Prices { get; }
    IWebsiteService Websites { get; }
    IWishlistService Wishlists { get; }
}