using App.Contracts.DAL.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    // list your repos here

    ICategoryRepository Categories { get; }
    IDiscRepository Discs { get; }
    IDiscFromPageRepository DiscFromPages { get; }
    IDiscsInWishlistRepository DiscsInWishlists { get; }
    IManufacturerRepository Manufacturers { get; }
    IPriceRepository Prices { get; }
    IWebsiteRepository Websites{ get; }

    IWishlistRepository Wishlists { get; }
    
    IEntityRepository<AppUser> Users { get; }
}