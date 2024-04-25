using Base.Contracts.DAL;
using DALDTO = App.DAL.DTO;

namespace App.Contracts.DAL.Repositories;

public interface IWebsiteRepository : IEntityRepository<DALDTO.Website>, IWebsiteRepositoryCustom<DALDTO.Website>
{
    // define your DAL only custom methods here
}

// define your shared (with bll) custom methods here
public interface IWebsiteRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}