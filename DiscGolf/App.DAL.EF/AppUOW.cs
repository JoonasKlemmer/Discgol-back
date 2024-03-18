using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    public AppUOW(AppDbContext dbContext) : base(dbContext)
    {
    }

    private ICategoryRepository? _categories;
    public ICategoryRepository Categories => _categories ?? new CategoryRepository(UowDbContext);
    
    private IManufacturerRepository? _manufacturers;
    public IManufacturerRepository Manufacturers => _manufacturers ?? new ManufacturerRepository(UowDbContext);
    
    private IWishlistRepository? _wishlists;
    public IWishlistRepository Wishlists => _wishlists ?? new WishlistRepository(UowDbContext);

    private IEntityRepository<AppUser>? _users;
    public IEntityRepository<AppUser> Users => _users ??
                                               new BaseEntityRepository<AppUser, AppUser, AppDbContext>(UowDbContext,
                                                   new DalDummyMapper<AppUser, AppUser>());
}