using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IDiscFromPageService : IEntityRepository<App.BLL.DTO.DiscFromPage>, IDiscFromPageRepositoryCustom<App.BLL.DTO.DiscFromPage>
{
    
}