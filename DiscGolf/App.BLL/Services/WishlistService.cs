
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;

using AutoMapper;
using Base.BLL;

using Wishlist = App.BLL.DTO.Wishlist;

namespace App.BLL.Services;

public class WishlistService :
    BaseEntityService<App.DAL.DTO.Wishlist, App.BLL.DTO.Wishlist, IWishlistRepository>,
    IWishlistService
{
    public WishlistService(IAppUnitOfWork uoW, IWishlistRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Wishlist, App.BLL.DTO.Wishlist>(mapper))
    {
    }

    public async Task<IEnumerable<Wishlist>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e))!;
    }
    
    public async Task<IEnumerable<Wishlist>> GetAll(Guid userId)
    {
        return (await Repository.GetAll(userId)).Select(e => Mapper.Map(e))!;
    }

}