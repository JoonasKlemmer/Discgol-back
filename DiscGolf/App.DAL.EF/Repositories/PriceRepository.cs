using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PriceRepository : BaseEntityRepository<APPDomain.Price, DALDTO.Price, AppDbContext>, IPriceRepository
{
    public PriceRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDomainMapper<APPDomain.Price, DALDTO.Price>(mapper))
    {
    }
    
    public async Task<IEnumerable<DALDTO.Price>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        query = query.OrderBy(c => c.Currency);
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e));
    }

    
    // implement your custom methods here
}