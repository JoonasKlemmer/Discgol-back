using App.BLL.DTO;
using DALDTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IManufacturerRepository : IEntityRepository<DALDTO.Manufacturer>, IManufacturerRepositoryCustom<DALDTO.Manufacturer>
{
    // define your DAL only custom methods here
}

// define your shared (with bll) custom methods here
public interface IManufacturerRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
    Task<IEnumerable<DALDTO.Manufacturer>> GetAllManufacturers();
}