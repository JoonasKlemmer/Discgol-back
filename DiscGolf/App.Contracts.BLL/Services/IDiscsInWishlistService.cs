using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;
namespace App.Contracts.BLL.Services;

public interface IDiscsInWishlistService : IEntityRepository<App.BLL.DTO.DiscsInWishlist>, IWishlistRepositoryCustom<App.BLL.DTO.DiscsInWishlist>
{
    
}