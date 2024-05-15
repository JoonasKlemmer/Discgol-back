using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;

using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using DiscsInWishlist = App.BLL.DTO.DiscsInWishlist;


namespace App.BLL.Services;

public class DiscsInWishlistService :
    BaseEntityService<App.DAL.DTO.DiscsInWishlist, App.BLL.DTO.DiscsInWishlist, IDiscsInWishlistRepository>,
    IDiscsInWishlistService
{
    public DiscsInWishlistService(IUnitOfWork uoW, IDiscsInWishlistRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.DiscsInWishlist, App.BLL.DTO.DiscsInWishlist>(mapper))
    {
    }

    public async Task<IEnumerable<DiscsInWishlist>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
    }

    


    public async Task<IEnumerable<DiscsInWishlist>> GetAllWithDetails(Guid userId)
    {
        return (await Repository.GetAllWithDetails(userId)).Select(e => Mapper.Map(e));
    }
    public async Task<IEnumerable<DiscsInWishlist>> GetAllWithDetailsNoUser()
    {
        return (await Repository.GetAllWithDetailsNoUser()).Select(e => Mapper.Map(e));
    }
}