using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IDiscService : IEntityRepository<App.BLL.DTO.Disc>, IDiscRepositoryCustom<App.BLL.DTO.Disc>
{
    
}
