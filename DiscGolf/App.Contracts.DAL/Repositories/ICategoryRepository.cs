using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ICategoryRepository : IEntityRepository<DALDTO.Category>, ICategoryRepositoryCustom<DALDTO.Category>
{
    // define your DAL only custom methods here
}

// define your shared (with bll) custom methods here
public interface ICategoryRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);


}