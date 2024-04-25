using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;
namespace App.Contracts.BLL.Services;

public interface IManufacturerService : IEntityRepository<App.BLL.DTO.Manufacturer>, IManufacturerRepositoryCustom<App.BLL.DTO.Manufacturer>
{
    
}