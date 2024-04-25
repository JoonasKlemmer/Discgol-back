using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;
using DALDTO = App.DAL.DTO;

public interface IDiscsInWishlistRepository  : IEntityRepository<DALDTO.DiscsInWishlist>, IDiscsInWishlistRepositoryCustom<DALDTO.DiscsInWishlist>
{
    // define your DAL only custom methods here
}

// define your shared (with bll) custom methods here
public interface IDiscsInWishlistRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}
