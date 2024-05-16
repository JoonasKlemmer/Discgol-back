using App.BLL.DTO;
using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;
namespace App.Contracts.BLL.Services;

public interface IWishlistService : IEntityRepository<App.BLL.DTO.Wishlist>, IWishlistRepositoryCustom<App.BLL.DTO.Wishlist>
{
    new Task<IEnumerable<Wishlist>> GetAll(Guid userId);
}