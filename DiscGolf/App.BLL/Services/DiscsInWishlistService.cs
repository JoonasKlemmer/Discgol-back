using App.BLL.DTO;
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
    private readonly IMapper _mapper;
    public DiscsInWishlistService(IUnitOfWork uoW, IDiscsInWishlistRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.DiscsInWishlist, App.BLL.DTO.DiscsInWishlist>(mapper))
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<DiscsInWishlist>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e))!;
    }

    public async Task<bool> GetDiscInWishlistById(Guid discFromPageId, Guid wishlistId)
    {
        return (await Repository.GetDiscInWishlistById(discFromPageId, wishlistId));
    }
    


    public async Task<List<DiscWithDetails>> GetAllWithDetails(Guid userId)
    {
        var discs = (await Repository.GetAllWithDetails(userId)).ToList();
        var discWd = new List<DiscWithDetails>();
        foreach (var disc in discs)
        {
            var discWithDetails = _mapper.Map<DiscWithDetails>(disc);
            discWd.Add(discWithDetails);
        }
        return discWd;
    }
}