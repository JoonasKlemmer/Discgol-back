using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    
    IWishlistService Wishlists { get; }
    ICategoryService Categories { get; }
    IManufacturerService Manufacturers { get; }
}