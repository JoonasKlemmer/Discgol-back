using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;
namespace App.Contracts.BLL.Services;

public interface IWebsiteService : IEntityRepository<App.BLL.DTO.Website>, IWishlistRepositoryCustom<App.BLL.DTO.Website>
{
    
}