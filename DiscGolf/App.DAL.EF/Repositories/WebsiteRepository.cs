using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WebsiteRepository : BaseEntityRepository<APPDomain.Website, DALDTO.Website, AppDbContext>, IWebsiteRepository
{
    public WebsiteRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDomainMapper<APPDomain.Website, DALDTO.Website>(mapper))
    {
    }
    
    public async Task<IEnumerable<DALDTO.Website>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        query = query.OrderBy(c => c.WebsiteName);
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e));
    }

    
    // implement your custom methods here
}