using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class DiscRepository : BaseEntityRepository<APPDomain.Disc, DALDTO.Disc, AppDbContext>, IDiscRepository
{
    public DiscRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDomainMapper<APPDomain.Disc, DALDTO.Disc>(mapper))
    {
    }
    
    public async Task<IEnumerable<DALDTO.Disc>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        query = query.OrderBy(c => c.Name);
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e));
    }

    public async Task<IEnumerable<DALDTO.Disc>> GetAllDiscs()
    {
        var query = CreateQuery();
        query = query.Include(d => d.Categories).Include(d => d.Manufacturer);
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e));
        
    }


    // implement your custom methods here
}