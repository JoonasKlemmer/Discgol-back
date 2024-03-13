using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class WishlistRepository : BaseEntityRepository<Wishlist, Wishlist, AppDbContext>, IWishlistRepository
{
    public WishlistRepository(AppDbContext dbContext) : 
        base(dbContext, new DalDummyMapper<Wishlist, Wishlist>())
    {
    }
    
    // implement your custom methods here
}