using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;
using DALDTO = App.DAL.DTO;

public interface IPriceRepository  : IEntityRepository<DALDTO.Price>, IPriceRepositoryCustom<DALDTO.Price>
{
    // define your DAL only custom methods here
}

// define your shared (with bll) custom methods here
public interface IPriceRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}
