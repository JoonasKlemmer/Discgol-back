using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class DiscsInWishlistRepository : BaseEntityRepository<APPDomain.DiscsInWishlist, DALDTO.DiscsInWishlist, AppDbContext>, IDiscsInWishlistRepository
{
    public DiscsInWishlistRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDomainMapper<APPDomain.DiscsInWishlist, DALDTO.DiscsInWishlist>(mapper))
    {
    }
    
    public async Task<IEnumerable<DALDTO.DiscsInWishlist>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        query = query.Where(c => c.Wishlists!.AppUserId == userId);
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e));
    }
    
    public async Task<IEnumerable<DALDTO.DiscsInWishlist>> GetAllWithDetails(Guid userId)
    {
        var query = CreateQuery(userId);
    
        query = query.Where(c => c.Wishlists!.AppUserId == userId);
        query = query
            .Include(c => c.DiscFromPage)
            .ThenInclude(c => c!.Discs).ThenInclude(c => c!.Manufacturers)
            .Include(c => c.Wishlists)
            .Include(c => c.DiscFromPage).ThenInclude(c => c!.Discs!.Categories)
            .Include(c => c.DiscFromPage).ThenInclude(c => c!.Websites);
    
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e))!;
    }
    
    public async Task<bool> GetDiscInWishlistById(Guid discFromPageId, Guid wishlistId)
    {
        var query = CreateQuery();
    
        query = query.Where(c => c.DiscFromPageId == discFromPageId &&  c.WishlistId == wishlistId );
        query = query
            .Include(c => c.DiscFromPage)
            .ThenInclude(c => c!.Discs).ThenInclude(c => c!.Manufacturers)
            .Include(c => c.Wishlists)
            .Include(c => c.DiscFromPage).ThenInclude(c => c!.Discs!.Categories)
            .Include(c => c.DiscFromPage).ThenInclude(c => c!.Websites);
    
        var res = await query.ToListAsync();
        return res.Count == 1;
    }
}