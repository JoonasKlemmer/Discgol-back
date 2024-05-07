using App.BLL.DTO;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WishlistRepository : BaseEntityRepository<APPDomain.Wishlist, DALDTO.Wishlist, AppDbContext>, IWishlistRepository
{
    public WishlistRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDomainMapper<APPDomain.Wishlist, DALDTO.Wishlist>(mapper))
    {
    }
    
    public async Task<IEnumerable<DALDTO.Wishlist>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        query = query.OrderBy(c => c.WishlistName);
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e));
    }

    
    // implement your custom methods here
}