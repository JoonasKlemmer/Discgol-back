using App.BLL.DTO;
using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IWishlistRepository : IEntityRepository<DALDTO.Wishlist>, IWishlistRepositoryCustom<DALDTO.Wishlist>
{
    Task<IEnumerable<DALDTO.Wishlist>> GetAll(Guid userId);
    // define your DAL only custom methods here
}

// define your shared (with bll) custom methods here
public interface IWishlistRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);

}