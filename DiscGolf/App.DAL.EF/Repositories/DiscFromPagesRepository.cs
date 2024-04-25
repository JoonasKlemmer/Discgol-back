using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class DiscFromPagesRepository : BaseEntityRepository<APPDomain.DiscFromPage, DALDTO.DiscFromPage, AppDbContext>, IDiscFromPageRepository
{
    public DiscFromPagesRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDomainMapper<APPDomain.DiscFromPage, DALDTO.DiscFromPage>(mapper))
    {
    }
    
    public async Task<IEnumerable<DALDTO.DiscFromPage>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        query = query.OrderBy(c => c.Price);
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e));
    }

    
    // implement your custom methods here
}